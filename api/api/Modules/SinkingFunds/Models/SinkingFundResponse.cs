namespace api.Modules.SinkingFunds.Models;

public class SinkingFundResponse
{
    public string Description { get; set; } = string.Empty;
    public decimal FinalSavings { get; set; }
    public decimal Target { get; init; }
    public decimal BaseAllocation { get; set; }
    public DateTime Deadline { get; set; }
    public int PoolPriority { get; set; }
    public FundRoleEnum Role { get; init; }
    public int MonthCompleted { get; set; }
    public bool Completed => FinalSavings >= Target;
}