using PROCommerce.Authentication.Domain.Entities.Base;

namespace PROCommerce.Authentication.Domain.Entities;

public class User : BaseEntity
{
    public string? Name { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }
}
