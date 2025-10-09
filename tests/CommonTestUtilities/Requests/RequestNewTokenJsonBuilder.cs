using CommonTestUtilities.Tokens.Refresh;
using MyMusicLibrary.Communication.Request;

namespace CommonTestUtilities.Requests;
public class RequestNewTokenJsonBuilder
{
    public static RequestNewTokenJson Build()
    {
        var refreshToken = RefreshTokenGeneratorBuilder.Build().Generate();

        var token = new RequestNewTokenJson()
        {
            RefreshToken = refreshToken
        };
        return token;
    }
}
