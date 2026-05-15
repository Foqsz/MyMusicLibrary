using CommonTestUtilities.Tokens.Generator;
using MyMusicLibrary.Exceptions;
using Shouldly;
using Xunit;

namespace WebApi.Test.Playlist.GetPlaylistId;
public class GetPlaylistByIdTest : MyLibraryMusicBookClassFixture
{
    private const string method = "playlist";
    private readonly long _playlistId;
    private readonly Guid _userIdentifier;

    public GetPlaylistByIdTest(CustomWebApplicationFactory factory) : base(factory) 
    {
        _playlistId = factory.GetPlaylistId();
        _userIdentifier = factory.GetUserIdentifier();
    }

    [Fact]
    public async Task Success()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var response = await DoGet($"{method}/{_playlistId}", token);

        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);
    }

    [Fact]
    public async Task Error_PlaylistId_NotFound()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var response = await DoGet($"{method}/{132}", token);

        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.NotFound);

        var content = await response.Content.ReadAsStringAsync();
        content.ShouldContain(ResourceMessagesException.PLAYLIST_NOTFOUND);
    }
}
