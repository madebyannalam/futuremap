using api.Common.ErrorHandling;
using api.Modules.SinkingFunds.Models;
using api.Modules.SinkingFunds.Services.Interfaces;

namespace api.Modules.SinkingFunds.Services;

public class SinkingFundsService : ISinkingFundsService
{
    public async Task<OperationResult<IList<SinkingFundAllocationResponse>>> GetAllocations(SinkingFundAllocationRequest request)
    {
        var result = new OperationResult<IList<SinkingFundAllocationResponse>>();
        return await Task.FromResult(result);
    }
}