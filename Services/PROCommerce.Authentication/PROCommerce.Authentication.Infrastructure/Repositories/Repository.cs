using PROCommerce.Authentication.Domain.Interfaces.Repositories;

namespace PROCommerce.Authentication.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext _dbContext;

    public Repository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<T> GetAll() => _dbContext.Set<T>();

    public T? Get(long id) => _dbContext.Set<T>().Find(id);

    public void Create(T entity)
    {
        _dbContext.Set<T>().Add(entity);
        _dbContext.SaveChanges();
    }

    public void Update(T entity)
    {
        _dbContext.Set<T>().Update(entity);
        _dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        T? entity = Get(id);

        if (entity == null)
            return;

        _dbContext.Set<T>().Remove(entity);
        _dbContext.SaveChanges();
    }
}