using System.Net;

namespace Randim.Common.Shared.Responses;

public class InternalServiceFailedResponse : BaseResponse
{
    public InternalServiceFailedResponse(Exception ex)
    {
        StatusCode = HttpStatusCode.InternalServerError;
        Messages = [ex.Message];
        IsSuccess = false;
    }
}
