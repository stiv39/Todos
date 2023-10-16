namespace Todos.Application.Common.Services;

public interface IPasswordService
{
    string EncryptPassword(Guid userId, string password, out string salt);
    bool VerifyPassword(Guid userId, string password, string hash, string salt);
}
