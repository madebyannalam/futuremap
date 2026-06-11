using api.Common.ErrorHandling;
using api.Modules.SinkingFunds.Models;
using api.Modules.SinkingFunds.Workers.Interfaces;

namespace api.Modules.SinkingFunds.Workers;

internal class ValidationWorker : IValidationWorker
{
    public OperationResult Validate(SimulationRequest request)
    {
        throw new NotImplementedException();
    }
}