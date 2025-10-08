using CommonTestUtilities.Tokens.Refresh;
using MyMusicLibrary.Domain.Entities;

namespace CommonTestUtilities.Entities;
public class RefreshTokenBuilder
{
    public static RefreshToken Build(User user)
    {
        return new RefreshToken
        {
            Value = RefreshTokenGeneratorBuilder.Build().Generate(),
            UserId = user.Id,
            User = user
        };
    }
}
