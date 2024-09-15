using Mapster;
using PROCommerce.Authentication.Domain.DTOs.Auth;
using PROCommerce.Authentication.Domain.DTOs.Auth.Response;
using PROCommerce.Authentication.Domain.Entities;
using PROCommerce.Authentication.Domain.Interfaces.Repositories;
using PROCommerce.Authentication.Domain.Interfaces.Services;

namespace PROCommerce.Authentication.Application.Services;

public class AuthServices : IAuthServices
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenServices _tokenService;

    public AuthServices(IUserRepository userRepository, ITokenServices tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public LoginResponseDTO Login(LoginDTO loginDTO)
    {
        try
        {
            User? user = _userRepository.GetByUsername(x => x.Username == loginDTO.Username)
                ?? throw new Exception("User not found.");

            if (user?.Password != loginDTO.Password)
                throw new Exception("Invalid credentials.");

            string token = _tokenService.GenerateToken(user!);

            LoginResponseDTO LoginResponseDTO = ValueTuple.Create(user, token).Adapt<LoginResponseDTO>();

            return LoginResponseDTO;
        }
        catch
        {
            throw;
        }

    }
}