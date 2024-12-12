using PROCommerce.Authentication.Domain.Enums;

namespace PROCommerce.Authentication.Domain.DTOs.Auth;

public class RegisterDTO
{
    public string FullName { get; set; }

    public string Username { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public UserRole Role { get; set; } = UserRole.Default;
}