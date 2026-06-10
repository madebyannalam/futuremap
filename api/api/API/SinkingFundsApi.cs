using api.Common.Extensions;
using api.Modules.SinkingFunds.Models;
using api.Modules.SinkingFunds.Services.Interfaces;

namespace api.API;

public static class SinkingFundsApi
{
    public static void MapSinkingFundsApi(this WebApplication app)
    {
        var group = app.MapGroup("sinking-funds");

        group.MapPost("/allocations", GetAllocations);
    }

    private static async Task<IResult> GetAllocations(ISinkingFundsService service, SimulationRequest request)
    {
        var result = await service.GetAllocations(request);
        return result.ToResult();
    }
}