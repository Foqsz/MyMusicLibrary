using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens.Generator;
using Shouldly;
using Xunit;

namespace WebApi.Test.Playlist;
public class CreatePlaylistTest : MyLibraryMusicBookClassFixture
{
    private const string method = "/Playlist/create";
    private readonly Guid _userIdentifier;
    public CreatePlaylistTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _userIdentifier = factory.GetUserIdentifier();
    }

    [Fact]
    public async Task Success()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var playlist = RequestCreatePlaylistJsonBuilder.Build();

        var response = await DoPost(method, playlist, token);

        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.Created);
        response.ShouldNotBeNull();
        response.ShouldSatisfyAllConditions();
    }
}
