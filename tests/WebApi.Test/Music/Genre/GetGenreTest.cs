using CommonTestUtilities.Tokens.Generator;
using Shouldly;
using Xunit;

namespace WebApi.Test.Music.Genre;
public class GetGenreTest : MyLibraryMusicBookClassFixture
{
    private const string method = "music/genre";
    private readonly Guid _userIdentifier;

    public GetGenreTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _userIdentifier = factory.GetUserIdentifier();
    }

    [Fact]
    public async Task Success()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var result = await DoGet(method, token);

        result.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);
        result.Content.ShouldNotBeNull();
    }
}
