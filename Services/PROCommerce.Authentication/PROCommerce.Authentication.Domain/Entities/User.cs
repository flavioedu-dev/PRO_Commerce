using PROCommerce.Authentication.Domain.Entities.Base;
using PROCommerce.Authentication.Domain.Enums;

namespace PROCommerce.Authentication.Domain.Entities;

public class User : BaseEntity
{
    public string? FullName { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public UserRole? Role { get; set; }
}
