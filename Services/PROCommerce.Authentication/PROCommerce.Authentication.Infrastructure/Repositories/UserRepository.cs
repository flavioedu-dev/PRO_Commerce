using PROCommerce.Authentication.Domain.Entities;
using PROCommerce.Authentication.Domain.Interfaces.Repositories;

namespace PROCommerce.Authentication.Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}