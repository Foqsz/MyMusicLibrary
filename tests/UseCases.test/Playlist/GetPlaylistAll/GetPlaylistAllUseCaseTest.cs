using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositores;
using MyMusicLibrary.Application.UseCases.Playlist.GetPlaylistAll;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;
using Shouldly;
using Xunit;

namespace UseCases.Test.Playlist.GetPlaylistAll;
public class GetPlaylistAllUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var user, var _) = UserBuilder.Build();
        var playlist = PlaylistBuilder.Collection(user);

        var useCase = CreateUseCase(user, playlist, playlistNull: false);

        var response = await useCase.Execute();

        response.ShouldNotBeNull();
        response.Playlists.Count.ShouldBeGreaterThan(0);

        foreach (var playlists in response.Playlists)
        {
            playlists.Name.ShouldNotBeNullOrWhiteSpace();
            playlists.Description.ShouldNotBeNullOrWhiteSpace();
            playlists.UserId.ShouldBe(user.Id);
            playlists.CreatedOn.ShouldNotBe(default(DateTime));
            playlists.OwnerName.ShouldNotBeNullOrWhiteSpace();
        }

    }

    [Fact]
    public async Task Error_Playlist_Null()
    {
        (var user, var _) = UserBuilder.Build();
        var playlist = PlaylistBuilder.Collection(user);

        var useCase = CreateUseCase(user, playlist, playlistNull: true);

        var exception = await Should.ThrowAsync<PlaylistException>(async () =>
        {
            await useCase.Execute();
        });

        exception.Message.ShouldBe(ResourceMessagesException.PLAYLISTS_ALL_NOTFOUND);
    }

    private static GetPlaylistAllUseCase CreateUseCase(MyMusicLibrary.Domain.Entities.User user, IList<MyMusicLibrary.Domain.Entities.Playlist> playlist, bool playlistNull)
    {
        var loggedUser = LoggedUserBuilder.Build(user);
        var playlistRepository = new PlaylistReadOnlyRepositoryBuilder();
        var userRepository = new UserReadOnlyRepositoryBuilder();

        if (playlistNull is false && playlist is not null)
            playlistRepository.GetAll(user, playlist);

        if(user is not null)
            userRepository.GetById(user);

        return new GetPlaylistAllUseCase(loggedUser, playlistRepository.Build(), userRepository.Build());
    }
}
