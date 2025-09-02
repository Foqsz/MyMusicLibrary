using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens.Generator;
using MyMusicLibrary.Exceptions;
using Shouldly;
using Xunit;

namespace WebApi.Test.Playlist.Create;
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

        var playlist = RequestFromPlaylistJsonBuilder.Build();

        var response = await DoPost(method, playlist, token);

        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.Created);
        response.ShouldNotBeNull();
        response.ShouldSatisfyAllConditions();
    }

    [Fact]
    public async Task Error_NamePlaylist_Long()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var playlist = RequestFromPlaylistJsonBuilder.Build();
        playlist.Name = new string('a', 101);

        var response = await DoPost(method, playlist, token);

        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.BadRequest);

        var content = await response.Content.ReadAsStringAsync();
        content.ShouldContain(ResourceMessagesException.PLAYLIST_NAME_TOO_LONG);
    }

    [Fact]
    public async Task Error_NamePlaylist_Empty()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var playlist = RequestFromPlaylistJsonBuilder.Build();
        playlist.Name = string.Empty;

        var response = await DoPost(method, playlist, token);

        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.BadRequest);

        var content = await response.Content.ReadAsStringAsync();
        content.ShouldContain(ResourceMessagesException.PLAYLIST_NAME_EMPTY);
    }

    [Fact]
    public async Task Error_Description_Long()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var playlist = RequestFromPlaylistJsonBuilder.Build();
        playlist.Description = new string('a', 501);

        var response = await DoPost(method, playlist, token);

        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.BadRequest);

        var content = await response.Content.ReadAsStringAsync();
        content.ShouldContain(ResourceMessagesException.PLAYLIST_DESCRIPTION_TOO_LONG);
    }
}
