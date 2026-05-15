using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens.Generator;
using MyMusicLibrary.Domain.Entities;
using MyMusicLibrary.Exceptions;
using Shouldly;
using Xunit;

namespace WebApi.Test.Playlist.RemoveMusicFromPlaylist;
public class RemoveMusicFromPlaylistTest : MyLibraryMusicBookClassFixture
{
    private const string method = "playlist/remove-music-playlist";
    private readonly Guid _userIdentifier;
    private readonly long _playlistId;
    private readonly long _musicId;

    public RemoveMusicFromPlaylistTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _userIdentifier = factory.GetUserIdentifier();
        _musicId = factory.GetMusicId();
        _playlistId = factory.GetPlaylistId();  
    }

    [Fact]
    public async Task Success()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var result = await DoDelete($"{method}?musicId={_musicId}&playlistId={_playlistId}", token);

        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(System.Net.HttpStatusCode.NoContent);

        result.IsSuccessStatusCode.ShouldBeTrue();
    }

    [Fact]
    public async Task Error_Music_NotFound()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var result = await DoDelete($"{method}?musicId={123}&playlistId={_playlistId}", token);

        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(System.Net.HttpStatusCode.BadRequest);

        var content = await result.Content.ReadAsStringAsync();
        content.ShouldContain(ResourceMessagesException.MUSIC_EMPTY);
    }

    [Fact]
    public async Task Error_Playlist_Invalid()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var result = await DoDelete($"{method}?musicId={_musicId}&playlistId={123}", token);

        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(System.Net.HttpStatusCode.NotFound);

        var content = await result.Content.ReadAsStringAsync();
        content.ShouldContain(ResourceMessagesException.ERROR_MUSIC_FROM_PLAYLIST);
    }
}
