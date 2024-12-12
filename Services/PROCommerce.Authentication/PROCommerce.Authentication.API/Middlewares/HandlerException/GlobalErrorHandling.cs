using PROCommerce.Authentication.API.Middlewares.HandlerException.Response;
using PROCommerce.Authentication.Domain.Exceptions;
using System.Text.Json;

namespace PROCommerce.Authentication.API.Middlewares.HandlerException;

public class GlobalErrorHandling
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public GlobalErrorHandling(RequestDelegate next, ILoggerFactory logger)
    {
        _next = next;
        _logger = logger.CreateLogger("LogError");
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(httpContext, ex);
        }
    }

    public async Task HandleErrorAsync(HttpContext httpContext, Exception ex)
    {
        _logger.LogError($"Error Message: {ex.Message}");
        _logger.LogError($"Inner Error Message: {ex.InnerException?.Message}");
        _logger.LogError($"Error Stack: {ex.StackTrace}");

        ErrorResponse errorResponse = new();

        httpContext.Response.ContentType = "application/json";

        switch (ex)
        {
            case CustomResponseException customException:
                errorResponse.Message = customException?.InnerException?.Message ?? customException?.Message;

                httpContext.Response.StatusCode = customException?.StatusCode ?? 400;

                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));

                break;

            default:
                errorResponse.Message = ex?.InnerException?.Message ?? ex?.Message;

                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));

                break;
        }
    }
}