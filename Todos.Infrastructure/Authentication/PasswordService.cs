using System.Security.Cryptography;
using System.Text;
using Todos.Application.Common.Services;

namespace Todos.Infrastructure.Authentication;

public class PasswordService : IPasswordService
{
    private const int KeySize = 64;
    private const int Iterations = 350000;
    private readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA512; 

    public string EncryptPassword(Guid userId, string password, out string salt)
    {
        var pepper = userId.ToByteArray();
        var passwordHash = Convert.ToHexString(CreatePassword(password, pepper, out var passwordSalt));
        salt =  passwordSalt;

        return passwordHash;
    }

    public bool VerifyPassword(Guid userId, string password, string hash, string salt)
    {
        var pepper = userId.ToByteArray();
        return Verify(password, hash, salt, pepper);
    }

    private bool Verify(string password, string hash, string salt, byte[] pepper) 
    {
        var hashToCompate = HashPassword(password, salt, pepper);
        return CryptographicOperations.FixedTimeEquals(hashToCompate, Convert.FromHexString(hash));
    }

    private byte[] CreatePassword(string password, byte[] pepper, out string salt)
    {
        var random = RandomNumberGenerator.GetBytes(KeySize);
        salt = Encoding.UTF8.GetString(random);


        return HashPassword(password, salt, pepper);
    }

    private byte[] HashPassword(string password, string salt, byte[] pepper)
    {
       
       return Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes(salt).Concat(pepper).ToArray(), Iterations, _hashAlgorithm, KeySize);
    }
}
