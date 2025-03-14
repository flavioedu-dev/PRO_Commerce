﻿using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
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
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenServices _tokenService;
    private readonly IPasswordEncryption _passwordEncryption;

    public AuthServices(IUnitOfWork unitOfWork, ITokenServices tokenService, IPasswordEncryption passwordEncryption)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
        _passwordEncryption = passwordEncryption;
    }

    public LoginResponseDTO Login(LoginDTO loginDTO)
    {
        User? user = _unitOfWork.UserRepository.GetByUsername(loginDTO.Username!)
            ?? throw new CustomResponseException(ApplicationMessages.Authentication_Login_User_NotFound, 401);

        bool passIsCorrect = _passwordEncryption.ComparePassword(loginDTO.Password!, user.Password!);

        if (!passIsCorrect)
            throw new CustomResponseException(ApplicationMessages.Authentication_Login_Credentials_Invalid, 401);

        string token = _tokenService.GenerateToken(user!);

        CookieOptions cookieOptions = new()
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict
        };

        loginDTO?.Cookies?.Append("Authorization", $"Bearer {token}", cookieOptions);

        LoginResponseDTO LoginResponseDTO = user.Adapt<LoginResponseDTO>();

        return LoginResponseDTO;
    }

    public RegisterResponseDTO Register(RegisterDTO registerDTO)
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

        RegisterResponseDTO registerResponseDTO = ValueTuple.Create(user, ApplicationMessages.Authentication_Register_User_Success).Adapt<RegisterResponseDTO>();

        return registerResponseDTO;
    }
}