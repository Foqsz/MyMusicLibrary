using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositores;
using MyMusicLibrary.Application.UseCases.Playlist.GetPlaylistId;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;
using Shouldly;
using Xunit;

namespace UseCases.Test.Playlist.GetPlaylistId;
public class GetPlaylistIdUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var user, var _) = UserBuilder.Build();

        var playlist = PlaylistBuilder.Build(user);

        var useCase = CreateUseCase(user, playlist);

        var result = await useCase.Execute(playlist.Id);

        result.ShouldNotBeNull();
        result.Description.ShouldBe(playlist.Description);
        result.Name.ShouldBe(playlist.Name);
        result.OwnerName.ShouldBe(user.Name);
        result.ShouldSatisfyAllConditions();
    }

    [Fact]
    public async Task Error_PlaylistId_NotFound()
    {
        (var user, var _) = UserBuilder.Build();

        var playlist = PlaylistBuilder.Build(user);

        var useCase = CreateUseCase(user, playlist);

        var exception = await Should.ThrowAsync<PlaylistException>(async () => await useCase.Execute(5));

        exception.GetStatusCode().ShouldBe(System.Net.HttpStatusCode.NotFound);
        exception.GetErrorMessages().ShouldContain(ResourceMessagesException.PLAYLIST_NOTFOUND);
    }

    private static GetPlaylistIdUseCase CreateUseCase(MyMusicLibrary.Domain.Entities.User user, MyMusicLibrary.Domain.Entities.Playlist playlistId)
    {
        var loggedUser = LoggedUserBuilder.Build(user);
        var playlistRepository = new PlaylistReadOnlyRepositoryBuilder();
        var musicRepository = new MusicReadOnlyRepositoryBuilder();

        if (playlistId is not null)
            playlistRepository.GetById(user, playlistId);

        return new GetPlaylistIdUseCase(loggedUser, playlistRepository.Build());
    }
}
