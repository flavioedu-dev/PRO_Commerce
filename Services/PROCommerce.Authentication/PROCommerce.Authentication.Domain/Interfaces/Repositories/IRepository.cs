using System.Linq.Expressions;

namespace PROCommerce.Authentication.Domain.Interfaces.Repositories;

public interface IRepository<T>
{
    IQueryable<T> GetAll();

    T? Get(long id);

    void Create(T entity);

    void Update(T entity);

    void Delete(int id);
}