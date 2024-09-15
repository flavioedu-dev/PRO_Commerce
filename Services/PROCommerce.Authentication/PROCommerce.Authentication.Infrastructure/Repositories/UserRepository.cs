using PROCommerce.Authentication.Domain.Entities;
using PROCommerce.Authentication.Domain.Interfaces.Repositories;
using System.Linq.Expressions;

namespace PROCommerce.Authentication.Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public User? GetByUsername(Expression<Func<User, bool>> predicate)
    {
        return _dbContext.Users.FirstOrDefault(predicate);
    }
}