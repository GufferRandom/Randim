using System.Net;

namespace Randim.Common.Shared.Responses;

public class BadRequestResponse : BaseResponse
{
    public BadRequestResponse(string? message)
    {
        StatusCode = HttpStatusCode.BadRequest;
        Messages = [message!];
        IsSuccess = false;
    }
}

public class BadRequestResponse<T> : BaseResponse<T>
{
    public BadRequestResponse(T data, string? message)
    {
        StatusCode = HttpStatusCode.BadRequest;
        Messages = [message!];
        IsSuccess = false;
        Data = data;
    }
}
