namespace api.Modules.SinkingFunds.Models;

public class SinkingFundRequest
{
    public string Description { get; set; } =  string.Empty;
    public decimal CurrentSavings { get; set; }
    public decimal Target { get; set; }
    public decimal BaseAllocation { get; set; }
    public DateTime? Deadline { get; set; }
    public int PoolPriority { get; set; }
    public FundRoleEnum FundRole { get; set; }
}