using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PROCommerce.Authentication.Application.Services;
using PROCommerce.Authentication.Domain.Configurations;
using PROCommerce.Authentication.Domain.Interfaces.Repositories;
using PROCommerce.Authentication.Domain.Interfaces.Services;
using PROCommerce.Authentication.Infrastructure.Repositories;
using System.Text;

namespace PROCommerce.Authentication.CrossCutting.IoC;

public static class PipelineExtensions
{
    public static void AddApplicationDI(this IServiceCollection services, IConfiguration configuration)
    {
        #region JWT
        services.AddScoped<ITokenServices, TokenServices>();

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetSection("AppSettings:TokenSecretKey")?.Value!)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        #endregion

        #region Auth
        services.AddScoped<IAuthServices, AuthServices>();
        #endregion Auth
    }

    public static void AddInfrastructureDI(this IServiceCollection services, IConfiguration configuration)
    {
        #region DbContext
        services.AddScoped<AppDbContext>();

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("PROCommerce"))
        );
        #endregion DbContext

        #region Repositories
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        #endregion Repositories
    }

    public static void AddConfigurationsDependeciesDI(this IServiceCollection services, IConfiguration configuration)
    {
        #region AppOptions
        services.Configure<AppOptions>(configuration.GetSection("AppSettings"));
        #endregion AppOptions
    }
}