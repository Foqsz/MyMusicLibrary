using MyMusicLibrary.Domain.Security.Tokens;
using MyMusicLibrary.Infrastructure.Security.Tokens.Access.Generator;

namespace CommonTestUtilities.Tokens.Generator;
public class JwtTokenGeneratorBuilder
{
    public static IAccessTokenGenerator Build() => new JwtTokenGenerator(expirationTimeMinutes: 5, signingKey: "tttttttttttttttttttttttttttttttt");
}
