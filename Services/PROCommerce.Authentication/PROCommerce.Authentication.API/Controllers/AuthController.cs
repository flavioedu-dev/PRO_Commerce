using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PROCommerce.Authentication.API.Models.Auth.Login;
using PROCommerce.Authentication.API.Models.Auth.Register;
using PROCommerce.Authentication.Domain.DTOs.Auth;
using PROCommerce.Authentication.Domain.DTOs.Auth.Response;
using PROCommerce.Authentication.Domain.Interfaces.Services;

namespace PROCommerce.Authentication.API.Controllers;

[Route("api")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthServices _authServices;

    public AuthController(IAuthServices authServices)
    {
        _authServices = authServices;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login(LoginModel loginModel)
    {
        LoginDTO loginDTO = loginModel.Adapt<LoginDTO>();

        LoginResponseDTO loginResponseDTO = _authServices.Login(loginDTO);

        return StatusCode(StatusCodes.Status200OK, loginResponseDTO);
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public IActionResult Register(RegisterModel registerModel)
    {
        RegisterDTO registerDTO = registerModel.Adapt<RegisterDTO>();

        RegisterResponseDTO registerResponseDTO = _authServices.Register(registerDTO);

        return StatusCode(StatusCodes.Status201Created, registerResponseDTO);
    }
}