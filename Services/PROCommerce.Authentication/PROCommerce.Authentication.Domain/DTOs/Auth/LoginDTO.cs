using Microsoft.AspNetCore.Http;

namespace PROCommerce.Authentication.Domain.DTOs.Auth;

public class LoginDTO
{
    public string? Username { get; set; }

    public string? Password { get; set; }

    public IResponseCookies? Cookies { get; set; }
}