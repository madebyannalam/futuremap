using api.Common.ErrorHandling;

namespace api.Common.Extensions;

public static class OperationResultExtensions
{
    public static IResult ToResult(this OperationResult result)
    {
        return result.Status switch
        {
            OperationStatusEnum.Success => TypedResults.Ok(),
            OperationStatusEnum.NotFound => TypedResults.NotFound(),
            OperationStatusEnum.ValidationError => TypedResults.BadRequest(),
            _ => TypedResults.InternalServerError()
        };
    }
    
    public static IResult ToResult<T>(this OperationResult<T> result)
    {
        return result.Status switch
        {
            OperationStatusEnum.Success => TypedResults.Ok(),
            OperationStatusEnum.NotFound => TypedResults.NotFound(),
            OperationStatusEnum.ValidationError => TypedResults.BadRequest(),
            _ => TypedResults.InternalServerError()
        };
    }
}