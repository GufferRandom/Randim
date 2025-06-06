using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Randim.Common.Shared.Responses;

namespace Randim.Common.Shared.Filters;

public class ApiResponseStatusCodeFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next
    )
    {
        var executedContext = await next();
        if (
            executedContext.Result is ObjectResult objectResult
            && objectResult.Value is BaseResponse baseResponse
        )
            executedContext.HttpContext.Response.StatusCode = (int)baseResponse.StatusCode;
    }
}
