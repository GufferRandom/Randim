using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Randim.Common.Shared.Responses;

namespace Randim.Common.Shared.Middlewares;

public class ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        switch (ex)
        {
            // case FluentValidation.ValidationException validationException:
            //     context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
            //     var dict = validationException
            //         .Errors.GroupBy(x => x.PropertyName)
            //         .ToDictionary(g => g.Key.Split('.').Last(), g => g.Select(x => x.ErrorMessage).ToArray());
            //     ValidationFailedResponse validationFailedResponse = new(dict);
            //     validationFailedResponse.Messages = ["Validation Failed"];
            //     return context.Response.WriteAsync(JsonSerializer.Serialize(validationFailedResponse));
            default:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return context.Response.WriteAsync(
                    JsonSerializer.Serialize(new InternalServiceFailedResponse(ex))
                );
        }
    }
}
