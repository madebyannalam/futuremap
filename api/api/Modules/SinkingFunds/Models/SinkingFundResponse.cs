namespace api.Modules.SinkingFunds.Models;

public class SinkingFundResponse
{
    public string Description { get; set; } = string.Empty;
    public decimal FinalSavings { get; set; }
    public decimal Target { get; set; }
    public int MonthCompleted { get; set; }
    public bool Completed { get; set; }
}