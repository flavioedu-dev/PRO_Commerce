using PROCommerce.Authentication.Domain.Entities;

namespace PROCommerce.Authentication.Domain.Interfaces.Services;

public interface ITokenServices
{
    string GenerateToken(User user);
}