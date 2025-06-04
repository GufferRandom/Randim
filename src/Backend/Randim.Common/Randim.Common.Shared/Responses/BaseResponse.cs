using System.Net;

namespace Randim.Common.Shared.Responses;

public class BaseResponse
{
    
    public HttpStatusCode StatusCode { get; set; }
    public List<string> Messages { get; set; }= [];
    public bool IsSuccess { get; set; }
}
public class BaseResponse<T> : BaseResponse
{
    public T? Data { get; set; }
}
