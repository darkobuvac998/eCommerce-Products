using eCommerce.Products.Domain.Exceptions;
using eCommerce.Products.Domain.Shared;
using Newtonsoft.Json;
using System.Net;

namespace eCommerce.Products.API.Middlewares;

public class GlobalExceptionHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        context.Response.StatusCode = exception switch
        {
            ValidationException => (int)HttpStatusCode.BadRequest,
            ItemNotFoundException => (int)HttpStatusCode.NotFound,
            _ => (int)HttpStatusCode.InternalServerError
        };

        var errorCode = exception switch
        {
            ValidationException => ErrorCodes.ValidationError,
            ItemNotFoundException => ErrorCodes.ItemNotFound,
            _ => ErrorCodes.InternalServerErrror
        };

        var error = new ErrorDetails { ErrorCode = errorCode, Message = exception.Message };

        var json = JsonConvert.SerializeObject(error);

        await context.Response.WriteAsync(json);
    }
}
