namespace api.Modules.SinkingFunds.Models;

public class SimulationResponse
{
    public int MonthsToComplete { get; set; }
    public decimal TotalAllocated { get; set; }
    public IList<SinkingFundResponse> SinkingFunds { get; set; } = new List<SinkingFundResponse>();
}