using PROCommerce.Authentication.Domain.DTOs.Auth;
using PROCommerce.Authentication.Domain.DTOs.Auth.Response;

namespace PROCommerce.Authentication.Domain.Interfaces.Services;

public interface IAuthServices
{
    LoginResponseDTO Login(LoginDTO loginDTO);
}
