using PROCommerce.Authentication.Domain.Entities;
using System.Linq.Expressions;

namespace PROCommerce.Authentication.Domain.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    User? GetByUsername(Expression<Func<User, bool>> predicate);
}
