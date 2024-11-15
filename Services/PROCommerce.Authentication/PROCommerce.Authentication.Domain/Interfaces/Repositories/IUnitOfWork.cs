namespace PROCommerce.Authentication.Domain.Interfaces.Repositories;

public interface IUnitOfWork : IDisposable
{
    IUserRepository UserRepository { get; }

    void Commit();
}