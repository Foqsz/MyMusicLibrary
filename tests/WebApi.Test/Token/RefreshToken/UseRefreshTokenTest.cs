using CommonTestUtilities.Entities;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens.Generator;
using MyMusicLibrary.Communication.Request;
using MyMusicLibrary.Exceptions;
using Shouldly;
using Xunit;

namespace WebApi.Test.Token.RefreshToken;
public class UseRefreshTokenTest : MyLibraryMusicBookClassFixture
{
    private const string METHOD = "/Token/refresh-token";
    private readonly Guid _userIdentifier;
    private readonly string _refreshToken;
    public DateTime _refreshTokenCreated;

    public UseRefreshTokenTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _userIdentifier = factory.GetUserIdentifier();
        _refreshToken = factory.GetRefreshToken();
        _refreshTokenCreated = factory.GetRefreshTokenCreated();
    }

    [Fact]
    public async Task Success()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var refreshToken = new RequestNewTokenJson()
        {
            RefreshToken = _refreshToken
        };

        var request = await DoPost(METHOD, refreshToken, token, "pt-BR");

        request.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);
        request.ShouldNotBeNull();

        var result = await request.Content.ReadAsStringAsync();

        result.ShouldContain("accessToken");
        result.ShouldContain("refreshToken");
    }

    [Fact]
    public async Task Error_Refresh_Token_Null()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var refreshToken = new RequestNewTokenJson()
        {
            RefreshToken = "123teste"
        };

        var request = await DoPost(METHOD, refreshToken, token, "en");

        request.StatusCode.ShouldBe(System.Net.HttpStatusCode.Unauthorized);

        var result = await request.Content.ReadAsStringAsync();

        result.ShouldContain(ResourceMessagesException.EXPIRED_SESSION);
    }
}
