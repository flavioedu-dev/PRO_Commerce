using Mapster;
using PROCommerce.Authentication.Domain.DTOs.Auth.Response;
using PROCommerce.Authentication.Domain.Entities;
using System.Reflection;

namespace PROCommerce.Authentication.API.Extentions.Mapper;

public static class MappingConfigurations
{
    public static void RegisterMaps(this IServiceCollection services)
    {
        TypeAdapterConfig<(User, string token), LoginResponseDTO>
            .NewConfig()
            .Map(dest => dest.Id, src => src.Item1.Id)
            .Map(dest => dest.Token, src => src.token);

        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
    }
}