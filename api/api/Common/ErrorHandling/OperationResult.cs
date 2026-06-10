namespace api.Common.ErrorHandling;

public class OperationResult
{
    public OperationStatusEnum Status { get; init; }
    
    public string? ErrorMessage { get; init; }
    
    public bool IsSuccess => Status == OperationStatusEnum.Success;

    public static OperationResult Success() => new()
    {
        Status = OperationStatusEnum.Success
    };

    public static OperationResult Failure(OperationStatusEnum status, string errorMessage) => new()
    {
        Status = status,
        ErrorMessage = errorMessage
    };
}

public class OperationResult<T> : OperationResult
{
    public T? Data { get; init; }

    public static OperationResult<T> Success(T data) => new()
    {
        Status = OperationStatusEnum.Success,
        Data = data
    };

    public new static OperationResult<T> Failure(OperationStatusEnum status, string errorMessage) => new()
    {
        Status = status,
        ErrorMessage = errorMessage
    };
}