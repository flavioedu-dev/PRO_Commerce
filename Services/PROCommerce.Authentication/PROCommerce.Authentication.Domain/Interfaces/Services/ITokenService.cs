using PROCommerce.Authentication.Domain.Entities;

namespace PROCommerce.Authentication.Domain.Interfaces.Services;

public interface ITokenService
{
    string GenerateToken(User user);
}