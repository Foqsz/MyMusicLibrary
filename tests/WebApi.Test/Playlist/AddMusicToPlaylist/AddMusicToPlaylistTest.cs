using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens.Generator;
using Shouldly;
using Xunit;

namespace WebApi.Test.Playlist.AddMusicToPlaylist;
public class AddMusicToPlaylistTest : MyLibraryMusicBookClassFixture
{
    private const string method = "playlist/add-music";
    private readonly Guid _userIdentifier;
    private readonly long _playlistId;
    private readonly long _musicId;

    public AddMusicToPlaylistTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _userIdentifier = factory.GetUserIdentifier();
        _musicId = factory.GetMusicId();
        _playlistId = factory.GetPlaylistId();
    }

    [Fact]
    public async Task Success()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var request = RequestMusicPlaylistJsonBuilder.Build();
        request.MusicId = _musicId;
        request.PlaylistId = _playlistId;

        var result = await DoPost(method, request, token);

        result.StatusCode.ShouldBe(System.Net.HttpStatusCode.Created);
    }

    [Fact]
    public async Task Error_PlaylistNotFound()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var request = RequestMusicPlaylistJsonBuilder.Build();
        request.MusicId = _musicId;
        request.PlaylistId = 2;

        var result = await DoPost(method, request, token);

        result.StatusCode.ShouldBe(System.Net.HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Error_MusicNotFound()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var request = RequestMusicPlaylistJsonBuilder.Build();
        request.MusicId = 3;
        request.PlaylistId = _playlistId;

        var result = await DoPost(method, request, token);

        result.StatusCode.ShouldBe(System.Net.HttpStatusCode.BadRequest);
    }
}
