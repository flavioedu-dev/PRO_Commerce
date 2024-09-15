namespace PROCommerce.Authentication.API.Extentions.IoC;

using PROCommerce.Authentication.API.Extentions.Mapper;
using PROCommerce.Authentication.CrossCutting.IoC;

public static class PipelineExtentions
{
    public static void AddApiDI(this IServiceCollection services)
    {
        #region Default
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        #endregion Default

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
}