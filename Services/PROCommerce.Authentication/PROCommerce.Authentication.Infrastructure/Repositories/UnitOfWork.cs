using PROCommerce.Authentication.Domain.Interfaces.Repositories;

namespace PROCommerce.Authentication.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;

    private IUserRepository? _userRepository;

    public UnitOfWork(AppDbContext appDbContext)
    {
        _dbContext = appDbContext;
    }

    public void Commit()
    {
        _dbContext.SaveChanges();
    }

    public void Dispose() => _dbContext.Dispose();

    public IUserRepository UserRepository
    {
        get 
        {
            return _userRepository ?? new UserRepository(_dbContext);
        }
    }
}