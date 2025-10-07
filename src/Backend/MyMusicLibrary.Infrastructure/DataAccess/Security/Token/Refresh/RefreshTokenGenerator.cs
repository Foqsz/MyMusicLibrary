using MyMusicLibrary.Domain.Security.Tokens;

namespace MyMusicLibrary.Infrastructure.DataAccess.Security.Token.Refresh;
public class RefreshTokenGenerator : IRefreshTokenGenerator
{
    public string Generate() => Convert.ToBase64String(Guid.NewGuid().ToByteArray());
}
