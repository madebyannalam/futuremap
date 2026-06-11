using api.Modules.SinkingFunds.Models;

namespace api.Modules.SinkingFunds.Workers.Interfaces;

public interface ISimulationWorker
{
    SimulationResponse Run(SimulationRequest request, IAllocationStrategy strategy);
}