namespace PROCommerce.Authentication.Domain.Interfaces.Services;

public interface IPasswordEncryption
{
    string HashPassword(string password);

    bool ComparePassword(string password, string hashDB);
}
