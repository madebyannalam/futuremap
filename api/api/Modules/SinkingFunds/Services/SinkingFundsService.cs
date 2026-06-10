using api.Common.ErrorHandling;
using api.Modules.SinkingFunds.Models;
using api.Modules.SinkingFunds.Services.Interfaces;

namespace api.Modules.SinkingFunds.Services;

public class SinkingFundsService : ISinkingFundsService
{
    public async Task<OperationResult<IList<SimulationResponse>>> GetAllocations(SimulationRequest request)
    {
        var result = new OperationResult<IList<SimulationResponse>>();
        return await Task.FromResult(result);
    }
}