namespace api.Common.ErrorHandling;

public class ValidationError
{
    public string PropertyPath { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}