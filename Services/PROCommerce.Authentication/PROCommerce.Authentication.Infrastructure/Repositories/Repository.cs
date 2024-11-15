using PROCommerce.Authentication.Domain.Interfaces.Repositories;

namespace PROCommerce.Authentication.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected AppDbContext? _dbContext;

    public IQueryable<T> GetAll() => _dbContext?.Set<T>()!;

    public T? Get(long id) => _dbContext?.Set<T>().Find(id);

    public void Create(T entity)
    {
        _dbContext?.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        _dbContext?.Set<T>().Update(entity);
    }

    public void Delete(int id)
    {
        T? entity = Get(id);

        if (entity == null)
            return;

        _dbContext?.Set<T>().Remove(entity);
    }
}