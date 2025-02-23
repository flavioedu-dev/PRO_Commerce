using Mapster;
using PROCommerce.Authentication.API.Models.Auth.Login;
using PROCommerce.Authentication.Domain.DTOs.Auth;
using PROCommerce.Authentication.Domain.DTOs.Auth.Response;
using PROCommerce.Authentication.Domain.Entities;
using System.Reflection;

namespace PROCommerce.Authentication.API.Extensions.Mapper;

public static class MappingConfigurations
{
    public static void RegisterMaps(this IServiceCollection _)
    {

        #region LoginModel -> LoginDTO
        TypeAdapterConfig<(LoginModel model, IResponseCookies cookies), LoginDTO>
            .NewConfig()
            .Map(dest => dest.Username, src => src.model.Username)
            .Map(dest => dest.Password, src => src.model.Password)
            .Map(dest => dest.Cookies, src => src.cookies);
        #endregion LoginModel -> LoginDTO

        #region User => LoginResponseDTO
        TypeAdapterConfig<(User, string token), LoginResponseDTO>
            .NewConfig()
            .Map(dest => dest.Id, src => src.Item1.Id);
        #endregion User => LoginResponseDTO

        #region RegisterDTO, hashPassword => User
        TypeAdapterConfig<(RegisterDTO registerDTO, string hashPassword), User>
            .NewConfig()
            .Map(dest => dest, src => src.registerDTO)
            .Map(dest => dest.Password, src => src.hashPassword);
        #endregion RegisterDTO, hashPassword => User

        #region User, Message => RegisterResponseDTO
        TypeAdapterConfig<(User user, string message), RegisterResponseDTO>
            .NewConfig()
            .Map(dest => dest.Id, src => src.user.Id)
            .Map(dest => dest.Message, src => src.message);
        #endregion User, Message => RegisterResponseDTO

        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
    }
}