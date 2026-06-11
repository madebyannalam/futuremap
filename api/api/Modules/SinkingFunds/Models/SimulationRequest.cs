namespace api.Modules.SinkingFunds.Models;

public class SimulationRequest
{
    public decimal AveragePoolAmount { get; set; }
    public bool VaryPoolAmount { get; set; } =  false;
    public AllocationStrategyEnum AllocationStrategy { get; set; }
    public IList<SinkingFundRequest> Funds { get; set; } = new List<SinkingFundRequest>();
}