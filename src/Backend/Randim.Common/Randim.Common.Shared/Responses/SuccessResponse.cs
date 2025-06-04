using System.Net;

namespace Randim.Common.Shared.Responses;

public class SuccessResponse:BaseResponse
{
    public SuccessResponse(string? message = null)
    {
        StatusCode = HttpStatusCode.OK;
        Messages = [message!];
        IsSuccess = true; 
    }
}
public class SuccessResponse<T>:BaseResponse<T>
{
    public SuccessResponse(T data,string? message = null)
    {
        StatusCode = HttpStatusCode.OK;
        Messages = [message!];
        IsSuccess = true;
        Data = data;
    }
}
