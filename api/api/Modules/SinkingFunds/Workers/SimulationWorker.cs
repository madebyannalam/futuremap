using api.Modules.SinkingFunds.Models;
using api.Modules.SinkingFunds.Workers.Interfaces;

namespace api.Modules.SinkingFunds.Workers;

internal class SimulationWorker : ISimulationWorker
{
    public SimulationResponse Run(SimulationRequest request, IAllocationStrategy strategy)
    {
        var result = new SimulationResponse();
        
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
         *              Calculate the 'amount owed'. I.e. the required amount - base allocation
         *              For those with a positive, non-zero amount owed:
         *                  Take the amount owed from the pool as a multiple of $10.
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
        
        return result;
    }

    internal void AllocateDumpingGroundFunds(SimulationRequest request, SinkingFundResponse dumpingGround)
    {
        /*
         * The vision:
         * No matter the progress, the dumping grounds gets the $1s & cents.
         */
    }
}