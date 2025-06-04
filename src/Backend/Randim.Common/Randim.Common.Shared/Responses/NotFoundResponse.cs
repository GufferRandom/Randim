using System.Net;

namespace Randim.Common.Shared.Responses;

public class NotFoundResponse:BaseResponse
{
    public NotFoundResponse()
    {
        StatusCode = HttpStatusCode.NotFound;
        IsSuccess = false;
        Messages = ["Not Found"];
    }
}

public class NotFoundResponse<T> : BaseResponse<T>
{
    public NotFoundResponse()
    {
        StatusCode = HttpStatusCode.NotFound;
        IsSuccess = false;
        Messages = ["Not Found"];
    }
}