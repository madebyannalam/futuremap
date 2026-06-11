using api.Modules.SinkingFunds.Models;
using api.Modules.SinkingFunds.Workers.Interfaces;

namespace api.Modules.SinkingFunds.Workers;

internal class SimulationWorker : ISimulationWorker
{
    public SimulationResponse Run(SimulationRequest request, IAllocationStrategy strategy)
    {
        throw new NotImplementedException();throw new NotImplementedException();
    }
}