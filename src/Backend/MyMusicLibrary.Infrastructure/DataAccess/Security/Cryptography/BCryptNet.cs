using MyMusicLibrary.Domain.Security.Cryptography;

namespace MyMusicLibrary.Infrastucture.Security.Cryptography;
public class BCryptNet : IPasswordEncripter
{
    public string Encrypt(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool IsValid(string password, string passwordHash)
    {
        //se a password que recebi da match com o hash que eu tenho
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}