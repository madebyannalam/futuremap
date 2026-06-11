using api.Common.ErrorHandling;
using api.Modules.SinkingFunds.Models;

namespace api.Modules.SinkingFunds.Workers.Interfaces;

public interface IValidationWorker
{
    OperationResult Validate(SimulationRequest request);
}