using MyMusicLibrary.Domain.Security.Tokens;
using MyMusicLibrary.Infrastructure.DataAccess.Security.Token.Refresh;

namespace CommonTestUtilities.Tokens.Refresh;
public class RefreshTokenGeneratorBuilder
{
    public static IRefreshTokenGenerator Build() => new RefreshTokenGenerator();
}
