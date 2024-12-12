using Mapster;
using PROCommerce.Authentication.Application.Resources;
using PROCommerce.Authentication.Domain.DTOs.Auth;
using PROCommerce.Authentication.Domain.DTOs.Auth.Response;
using PROCommerce.Authentication.Domain.Entities;
using PROCommerce.Authentication.Domain.Exceptions;
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
                ?? throw new CustomResponseException(ApplicationMessages.Authentication_Login_User_NotFound);

            if (user?.Password != loginDTO.Password)
                throw new CustomResponseException(ApplicationMessages.Authentication_Login_ValidCredentials_Fail);

            string token = _tokenService.GenerateToken(user!);

            LoginResponseDTO LoginResponseDTO = ValueTuple.Create(user, token).Adapt<LoginResponseDTO>();

            return LoginResponseDTO;
        }
        catch
        {
            throw;
        }

    }

    public RegisterResponseDTO Register(RegisterDTO registerDTO)
    {
        try
        {
            User? userByUsername = _unitOfWork.UserRepository.GetByUsername(registerDTO.Username);

            if (userByUsername is not null) 
                throw new CustomResponseException(ApplicationMessages.Authentication_Register_User_UsernameExists);

            User? userByEmail = _unitOfWork.UserRepository.GetByEmail(registerDTO.Email);

            if (userByEmail is not null) 
                throw new CustomResponseException(ApplicationMessages.Authentication_Register_User_EmailExists);

            string hashPassword = _passwordEncryption.HashPassword(registerDTO.Password);

            User user = ValueTuple.Create(registerDTO, hashPassword).Adapt<User>();

            _unitOfWork.UserRepository.Create(user);
            _unitOfWork.Commit();

            RegisterResponseDTO registerResponseDTO = user.Adapt<RegisterResponseDTO>();

            return registerResponseDTO;
        }
        catch
        {
            throw;
        }
    }
}