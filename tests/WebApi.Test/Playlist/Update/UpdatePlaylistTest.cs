using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens.Generator;
using MyMusicLibrary.Communication.Request;
using MyMusicLibrary.Exceptions;
using Shouldly;
using Xunit;

namespace WebApi.Test.Playlist.Update;
public class UpdatePlaylistTest : MyLibraryMusicBookClassFixture
{
    private const string method = "playlist";
    private readonly Guid _userIdentifier;
    private readonly string _playlistName;
    private readonly string _playlistDescription;
    private readonly long _playlistId;

    public UpdatePlaylistTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _userIdentifier = factory.GetUserIdentifier();
        _playlistName = factory.GetPlaylistName();
        _playlistDescription = factory.GetPlaylistDescription();
        _playlistId = factory.GetPlaylistId();
    }

    [Fact]
    public async Task Success()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(userIdentifier: _userIdentifier);

        var newPlaylist = RequestFromPlaylistJsonBuilder.Build();

        var response = await DoPut($"{method}/{_playlistId}", newPlaylist, token);

        response.EnsureSuccessStatusCode();
        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);
    }

    //[Fact]
    //public async Task Error_Dados_NoChanges()
    //{
    //    var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

    //    // Criar request com os mesmos dados da playlist existente
    //    var updateRequest = new RequestFromPlaylistJson
    //    {
    //        Name = _playlistName,
    //        Description = _playlistDescription
    //    };

    //    var response = await DoPut($"{method}/{_playlistId}", updateRequest, token);

    //    response.StatusCode.ShouldBe(System.Net.HttpStatusCode.BadRequest);
    //    var content = await response.Content.ReadAsStringAsync();
    //    content.ShouldContain(ResourceMessagesException.UPDATE_ERROR);
    //}


    [Fact]
    public async Task Error_Id_Playlist_NotFound()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(userIdentifier: _userIdentifier);

        var newPlaylist = RequestFromPlaylistJsonBuilder.Build();

        var createPlaylist = await DoPost("playlist/create", newPlaylist, token);

        var response = await DoPut($"{method}/{5}", newPlaylist, token);

        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.NotFound);

        var content = await response.Content.ReadAsStringAsync();
        content.ShouldContain(ResourceMessagesException.PLAYLIST_NOTFOUND);
    }
}
