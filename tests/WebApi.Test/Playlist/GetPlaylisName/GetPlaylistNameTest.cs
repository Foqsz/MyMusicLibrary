using CommonTestUtilities.Tokens.Generator;
using MyMusicLibrary.Exceptions;
using Shouldly;
using Xunit;

namespace WebApi.Test.Playlist.GetPlaylisName;
public class GetPlaylistNameTest : MyLibraryMusicBookClassFixture
{
    private const string method = "playlist/search";
    private readonly Guid _userIdentifier;
    private readonly string _playlistName;
    public GetPlaylistNameTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _userIdentifier = factory.GetUserIdentifier();
        _playlistName = factory.GetPlaylistName();
    }

    [Fact]
    public async Task Success()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var response = await DoGet($"{method}?name={_playlistName}", token);

        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);
    }

    [Fact]
    public async Task Error_Playlist_NotFound()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var response = await DoGet($"{method}?name={"oioi"}", token);

        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.NotFound);

        var content = await response.Content.ReadAsStringAsync();
        content.ShouldContain(ResourceMessagesException.PLAYLIST_NOTFOUND);
    }
}
