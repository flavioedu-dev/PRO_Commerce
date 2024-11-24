using FluentValidation;
using FluentValidation.AspNetCore;
using PROCommerce.Authentication.API.Extentions.Mapper;
using PROCommerce.Authentication.API.Middlewares;
using PROCommerce.Authentication.CrossCutting.IoC;

namespace PROCommerce.Authentication.API.Extentions.IoC;

public static class PipelineExtentions
{
    public static void AddApiDI(this IServiceCollection services)
    {
        #region Default
        services.AddControllers();
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