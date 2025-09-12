using CommonTestUtilities.Tokens.Generator;
using MyMusicLibrary.Exceptions;
using Shouldly;
using Xunit;

namespace WebApi.Test.Playlist.GetPlaylistAll;
public class GetPlaylistAllTest : MyLibraryMusicBookClassFixture
{
    private const string method = "playlist";
    private readonly Guid _userIdentifier;
    private readonly long _playlistId;
    public GetPlaylistAllTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _userIdentifier = factory.GetUserIdentifier();
        _playlistId = factory.GetPlaylistId();
    }

    [Fact]
    public async Task Success()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var response = await DoGet(method, token);

        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);
    }

    [Fact]
    public async Task Error_Playlist_NotFound()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var delete = await DoDelete($"{method}/{_playlistId}", token);

        delete.StatusCode.ShouldBe(System.Net.HttpStatusCode.NoContent);

        var response = await DoGet(method, token);

        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.NotFound);

        var content = await response.Content.ReadAsStringAsync();

        content.ShouldContain(ResourceMessagesException.PLAYLISTS_ALL_NOTFOUND);
    }
}
