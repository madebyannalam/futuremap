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
        // Each sinking fund must:
        //  Have a description/name
        //  Have a target amount
        //  Have a current savings specified
        //  Have a base allocation
        //  Have a pool priority specified
        // Only 1 fund can be named as the dumping ground. If only 1 fund is set up, it IS the dumping ground.
        //  E.g. You're modelling the savings for a home deposit
        
        foreach (var fund in request.Funds)
        {
            //
        }
        
        return OperationResult.Success();
    }
}