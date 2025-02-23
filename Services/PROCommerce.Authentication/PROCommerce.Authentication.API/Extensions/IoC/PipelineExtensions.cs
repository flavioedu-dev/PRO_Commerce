using FluentValidation;
using FluentValidation.AspNetCore;
using PROCommerce.Authentication.API.Extensions.Mapper;
using PROCommerce.Authentication.API.Filters;
using PROCommerce.Authentication.API.Middlewares;
using PROCommerce.Authentication.CrossCutting.IoC;

namespace PROCommerce.Authentication.API.Extensions.IoC;

public static class PipelineExtensions
{
    public static void AddApiDI(this IServiceCollection services)
    {
        #region Default
        services.AddControllers(options =>
            options.Filters.Add(typeof(CustomErrorResponse))
        ).ConfigureApiBehaviorOptions(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        #endregion Default

        #region FluentValidation
        services.AddValidatorsFromAssembly(typeof(Program).Assembly);
        services.AddFluentValidationAutoValidation();
        #endregion FluentValidation

        #region Mapster
        services.RegisterMaps();
        #endregion Mapster
    }

    public static void AddDI(this IServiceCollection services, IConfiguration config)
    {
        services.AddApiDI();
        services.AddApplicationDI(config);
        services.AddInfrastructureDI(config);
        services.AddConfigurationsDependeciesDI(config);
    }

    public static void AddMiddlewares(this IApplicationBuilder app)
    {
        app.UseGlobalErrorHandling();
    }
}