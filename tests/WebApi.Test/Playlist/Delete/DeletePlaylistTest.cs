using CommonTestUtilities.Tokens.Generator;
using Shouldly;
using Xunit;

namespace WebApi.Test.Playlist.Delete;
public class DeletePlaylistTest : MyLibraryMusicBookClassFixture
{
    private const string method = "Playlist/delete";
    private readonly Guid _userIdentifier;
    private readonly long _playlistId;

    public DeletePlaylistTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _userIdentifier = factory.GetUserIdentifier();
        _playlistId = factory.GetPlaylistId();
    } 

    [Fact]
    public async Task Success()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var response = await DoDelete($"{method}?playlistId={_playlistId}", token, culture: "pt-BR");

        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Error_Playlist_NotFound()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var response = await DoDelete($"{method}?playlistId={123}", token, culture: "pt-BR");

        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.NotFound);
    }
}
