using PROCommerce.Authentication.API.Middlewares.HandlerException;

namespace PROCommerce.Authentication.API.Middlewares;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalErrorHandling(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<GlobalErrorHandling>();
    }
}
