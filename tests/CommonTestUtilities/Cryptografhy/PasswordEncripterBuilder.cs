using MyMusicLibrary.Domain.Security.Cryptography;
using MyMusicLibrary.Infrastucture.Security.Cryptography;

namespace CommonTestUtilities.Cryptografhy;
public class PasswordEncripterBuilder
{
    public static IPasswordEncripter Build() => new BCryptNet();
}
