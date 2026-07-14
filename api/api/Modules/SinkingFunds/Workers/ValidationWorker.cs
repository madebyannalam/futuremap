using api.Common.ErrorHandling;
using api.Modules.SinkingFunds.Models;
using api.Modules.SinkingFunds.Workers.Interfaces;

namespace api.Modules.SinkingFunds.Workers;

internal class ValidationWorker : IValidationWorker
{
    public OperationResult Validate(SimulationRequest request)
    {
        if (!request.Funds.Any())
        {
            return OperationResult.Failure(OperationStatusEnum.ValidationError, new List<ValidationError>
            {
                new()
                {
                    PropertyPath = "Funds",
                    Message = "At least one fund is required."
                }
            });
        }
        
        if (request.AveragePoolAmount == 0)
        {
            return OperationResult.Failure(OperationStatusEnum.ValidationError, new List<ValidationError>
            {
                new()
                {
                    PropertyPath = "AveragePoolAmount",
                    Message = "The average pool amount must be greater than zero."
                }
            });
        }

        if (request.Funds.Count(f => f.FundRole == FundRoleEnum.DumpingGround) > 1)
        {
            return OperationResult.Failure(OperationStatusEnum.ValidationError, new List<ValidationError>
            {
                new()
                {
                    PropertyPath = "Funds",
                    Message = "Only one fund can be designated as the dumping ground"
                }
            });
        }
        
        // Each sinking fund must:
        //  Have a description/name
        //  Have a target amount
        //  Have a current savings specified
        //  Have a base allocation
        //  Have a pool priority specified
        //  E.g. You're modelling the savings for a home deposit
        
        var validationErrors = new List<ValidationError>();
        for (var i = 1; i <= request.Funds.Count; i++)
        {
            var fund = request.Funds[i - 1];
            var errors = new List<ValidationError>();

            if (string.IsNullOrWhiteSpace(fund.Description))
            {
                errors.Add(new ValidationError
                    {
                        PropertyPath = $"Funds[{i}].Description",
                        Message = $"The Description for the fund at position {i} is required."
                    }
                );
            }

            if (fund.Target < 0)
            {
                errors.Add(new ValidationError
                    {
                        PropertyPath = $"Funds[{i}].Target",
                        Message = $"The Target for the fund at position {i} is invalid."
                    }
                );
            }

            if (fund.CurrentSavings < 0)
            {
                errors.Add(new ValidationError
                    {
                        PropertyPath = $"Funds[{i}].CurrentSavings",
                        Message = $"The current savings for the fund at position {i} must be greater than zero."
                    }
                );
            }

            if (fund.BaseAllocation < 0)
            {
                errors.Add(new ValidationError
                    {
                        PropertyPath = $"Funds[{i}].BaseAllocation",
                        Message = $"The base allocation for the fund at position {i} must be greater than zero."
                    }
                );
            }

            if (fund.PoolPriority < 0 || fund.PoolPriority > request.Funds.Count)
            {
                errors.Add(new ValidationError
                    {
                        PropertyPath = $"Funds[{i}].PoolPriority",
                        Message = $"The pool priority for the fund at position {i} must be between 1 and {request.Funds.Count}."
                    }
                );
            }
            
            if (errors.Any())
            {
                validationErrors.AddRange(errors);
            }
        }

        return validationErrors.Any() ? OperationResult.Failure(OperationStatusEnum.ValidationError, validationErrors) : OperationResult.Success();
    }
}