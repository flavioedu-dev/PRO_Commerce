using PROCommerce.Authentication.Domain.Entities;
using System.Linq.Expressions;

namespace PROCommerce.Authentication.Domain.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    User? GetByUsername(string username);

    User? GetByEmail(string email);
}
