using api.Modules.SinkingFunds.Models;
using api.Modules.SinkingFunds.Workers.Interfaces;

namespace api.Modules.SinkingFunds.Workers;

internal class SimulationWorker : ISimulationWorker
{
    public SimulationResponse Run(SimulationRequest request, IAllocationStrategy strategy)
    {
        var result = new SimulationResponse
        {
            MonthsToComplete = 0,
            TotalAllocated = 0,
            SinkingFunds = request.Funds.Select(f => new SinkingFundResponse
            {
                Description = f.Description,
                FinalSavings = 0,
                Target = f.Target,
                Role = f.FundRole,
                BaseAllocation = f.BaseAllocation,
                PoolPriority = f.PoolPriority
            }).ToList()
        };

        var month = 0;
        while (!result.AllCompleted)
        {
            result.MonthsToComplete++;
            
            var averagePoolAmount = request.AveragePoolAmount;
            
            // Give the $1s and cents to the dumping ground
            var dumpingGround = result.SinkingFunds.SingleOrDefault(f => f.Role == FundRoleEnum.DumpingGround);
            dumpingGround?.FinalSavings += request.AveragePoolAmount % 10;

            // Get the list of completed funds and give their base allocations to the pool
            // These should be the funds that were completed in prior months
            averagePoolAmount += result.SinkingFunds.Where(f => f.Completed).Sum(f => f.BaseAllocation);
            
            // Get the incomplete funds. These should be funds that can still be added to this month.
            var incompleteFunds = result.SinkingFunds
                .Where(f => !f.Completed)
                .OrderBy(f => f.Deadline)
                .ThenBy(f => f.PoolPriority);
            
            var deadlineDrivenFunds = incompleteFunds.Where(f => f.Deadline != DateTime.MinValue);

            foreach (var fund in incompleteFunds)
            {
                var requiredMonthlyAmount = fund.BaseAllocation;

                if (fund.Deadline != DateTime.MinValue)
                {
                    decimal months = (fund.Deadline.Year - DateTime.Now.Year) * 12 + fund.Deadline.Month - DateTime.Now.Month;
                    requiredMonthlyAmount = (fund.Target - fund.FinalSavings) / months;
                }
            
                // Apply the base allocations
                AllocateToFund(fund, fund.BaseAllocation, result.MonthsToComplete);
                
                // If the fund has been completed by its base allocation OR
                // the base allocation is basically the same or greater than the required monthly amount, then we skip to the next one
                if (fund.Completed || fund.BaseAllocation >= requiredMonthlyAmount) continue;

                if (fund.Deadline != DateTime.MinValue)
                {
                    // Calculate the amount owed if we have a fund with a deadline
                    // Based on the above sorting, they should come to the top of the list
                    var amountOwed = Math.Min(requiredMonthlyAmount - fund.BaseAllocation, averagePoolAmount);
                
                    // If it is greater than zero, move that amount from the pool to that fund
                    if (amountOwed > 0)
                    {
                        AllocateToFund(fund, amountOwed, result.MonthsToComplete);
                        averagePoolAmount -= amountOwed;
                    }
                }

                if (fund.Deadline == DateTime.MinValue && averagePoolAmount > 0)
                {
                    var amountToGo = fund.Target - fund.FinalSavings;
                    if (amountToGo >= averagePoolAmount)
                    {
                        AllocateToFund(fund, averagePoolAmount, result.MonthsToComplete);
                        averagePoolAmount = 0;
                    }
                    else
                    {
                        var allocation = Math.Floor(amountToGo / 10m) * 10m;
                        AllocateToFund(fund, allocation, result.MonthsToComplete);
                        averagePoolAmount -=  allocation;
                    }
                }
            }
        }
        
        return result;
    }

    private void AllocateToFund(SinkingFundResponse fund, decimal amount, int currentMonth)
    {
        if (amount <= 0) return;
        
        fund.FinalSavings += amount;

        if (fund.MonthCompleted <= 0 && fund.FinalSavings >= fund.Target)
        {
            fund.MonthCompleted = currentMonth;
        }
    }
}
        
        /*
         * While there are still active funds:
         *      For each month:
         *          Send the $1s & cents to the dumping ground
         *          The remainder should be a  multiple of $10.
         *          Get a list of the complete funds.
         *              Give their base allocations to the pool.
         *          Get a list of the active funds
         *          Calculate the required monthly amount
         *              For deadline drive funds, this will be the balance required divided by the months between now and the deadline.
         *              For the non-deadline funds, this will be its base allocation
         *          Give those funds their base allocations in $10s
         *              If the base allocation is more than the amount owed, the excess joins the pool.
         *          Filter out funds that have been completed by the current month's base allocation.
         *          Split it out into those with deadlines and those without
         *          For the deadline driven funds:
         *              Calculate the 'amount owed' (rounded up to the nearest $10). I.e. the required amount - base allocation
         *              For those with a positive, non-zero amount owed:
         *                  Take the amount owed from the pool.
         *          For the non-deadline funds:
         *              Confirm that we have non-deadline funds AND still have funds in the pool.
         *              If both are true:
         *                  Decide on which allocation strategy to use. At the moment, we are going with ye olde snowball.                  
         *                  For the snowball
         *                      Order the funds in ascending order by target amount
         *                      If the pool amount is less than the amount to go for the first fund in line, then that fund gets the entirety of the pool.
         *                      Otherwise, the fund in question gets the multiples of $10 required to complete it. The surplus goes to the next fund in line.
         *                  For the other funds
         *                      Order the non-deadline funds by FundRole, then by priority
         */