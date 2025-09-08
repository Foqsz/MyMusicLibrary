using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositores;
using MyMusicLibrary.Application.UseCases.Playlist.GetPlaylistName;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;
using Shouldly;
using Xunit;

namespace UseCases.Test.Playlist;
public class GetPlaylistNameUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var user, var _) = UserBuilder.Build();
        var playlists = PlaylistBuilder.Collection(user);
        var playlistName = playlists.First().Name;

        var useCase = CreateUseCase(
            new[] { user },  
            playlists,
            playlistName
        );

        var result = await useCase.Execute(playlistName);

        result.ShouldNotBeNull();

        result.Playlists.ShouldBe(result.Playlists);
        result.Playlists[0].Name.ShouldBe(playlistName);

    }

    [Fact]
    public async Task Error_Playlist_NotFound()
    {
        (var user, var _) = UserBuilder.Build();
        var playlists = PlaylistBuilder.Collection(user);
        var playlistName = playlists.First().Name;

        var useCase = CreateUseCase(
            new[] { user },
            playlists,
            playlistName
        );

        var exception = await Should.ThrowAsync<PlaylistException>(async () =>
        {
            await useCase.Execute("oi");
        });

        exception.Message.ShouldBe(ResourceMessagesException.PLAYLIST_NOTFOUND);

    }

    private static GetPlaylistNameUseCase CreateUseCase(IEnumerable<MyMusicLibrary.Domain.Entities.User> user, 
        IList<MyMusicLibrary.Domain.Entities.Playlist> playlists, 
        string playlistName)
    {
        var playlistRepositoryReadOnly = new PlaylistReadOnlyRepositoryBuilder();
        var userRepositoryReadOnly = new UserReadOnlyRepositoryBuilder();

        userRepositoryReadOnly.GetByIds(user);
        playlistRepositoryReadOnly.GetByName(playlists, playlistName);

        return new GetPlaylistNameUseCase(playlistRepositoryReadOnly.Build(), userRepositoryReadOnly.Build());
    }
}
