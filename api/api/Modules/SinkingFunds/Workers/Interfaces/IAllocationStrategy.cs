using api.Modules.SinkingFunds.Models;

namespace api.Modules.SinkingFunds.Workers.Interfaces;

public interface IAllocationStrategy
{
    void Allocate(SimulationRequest request);
}