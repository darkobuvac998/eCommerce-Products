using eCommerce.Products.Domain.Exceptions;
using eCommerce.Products.Domain.Shared;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace eCommerce.Products.API.Middlewares;

public class GlobalExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

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
        _logger.LogError(json);
        _logger.LogError(
            "Exception occured: {@Exception} {@InnerException} at {@DateTimeUtc}",
            exception,
            exception?.InnerException,
            DateTime.UtcNow
        );

        await context.Response.WriteAsync(json);
    }
}
