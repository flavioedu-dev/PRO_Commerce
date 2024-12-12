using PROCommerce.Authentication.Domain.DTOs.Auth;
using PROCommerce.Authentication.Domain.Entities;
using PROCommerce.Authentication.Domain.Interfaces.Repositories;
using System.Linq.Expressions;

namespace PROCommerce.Authentication.Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public User? GetByUsername(string username)
    {
        return _dbContext?.Users.FirstOrDefault(x => x.Username == username);
    }

    public User? GetByEmail(string email)
    {
        return _dbContext?.Users.FirstOrDefault(x => x.Email == email);
    }
}