using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PROCommerce.Authentication.Domain.Configurations;
using PROCommerce.Authentication.Domain.Entities;
using PROCommerce.Authentication.Domain.Interfaces.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PROCommerce.Authentication.Application.Services;

public class TokenService : ITokenService
{
    private readonly AppOptions _appOptions;

    public TokenService(IOptions<AppOptions> appOptions)
    {
        _appOptions = appOptions.Value;
    }

    public string GenerateToken(User user)
    {
        JwtSecurityTokenHandler tokenHandler = new();

        byte[] key = Encoding.ASCII.GetBytes(_appOptions.TokenSecretKey!);

        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()!)
            }),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new(key: new SymmetricSecurityKey(key), algorithm: SecurityAlgorithms.HmacSha256Signature)
        };

        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
