namespace api.Modules.SinkingFunds.Models;

public class SimulationRequest
{
    public decimal Surplus { get; set; }
    public bool VarySurplus { get; set; } =  false;
    public IList<SinkingFundRequest> Funds { get; set; } = new List<SinkingFundRequest>();
}