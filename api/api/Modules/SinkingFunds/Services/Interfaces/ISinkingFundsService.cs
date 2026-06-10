using api.Common.ErrorHandling;
using api.Modules.SinkingFunds.Models;

namespace api.Modules.SinkingFunds.Services.Interfaces;

public interface ISinkingFundsService
{
    Task<OperationResult<IList<SinkingFundAllocationResponse>>> GetAllocations(SinkingFundAllocationRequest request);
}