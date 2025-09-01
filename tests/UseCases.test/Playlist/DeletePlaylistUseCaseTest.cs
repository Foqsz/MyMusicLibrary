using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositores;
using CommonTestUtilities.Requests;
using MyMusicLibrary.Application.UseCases.Playlist.Delete;
using MyMusicLibrary.Communication.Request;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;
using Shouldly;
using Xunit;

namespace UseCases.Test.Playlist;
public class DeletePlaylistUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var user, var _) = UserBuilder.Build();

        var playlist = PlaylistBuilder.Build(user);

        var useCase = CreateUseCase(user, playlist);    

        var result = useCase.Execute(playlist.Id);

        await result.ShouldNotBeNull();
        await result.ShouldNotThrowAsync();
    }

    [Fact]
    public async Task Error_Playlist_NotFound()
    {
        (var user, var _) = UserBuilder.Build();

        var playlist = PlaylistBuilder.Build(user);

        var useCase = CreateUseCase(user, playlist);

        var exception = await Should.ThrowAsync<PlaylistException>(async () => await useCase.Execute(5));

        exception.GetStatusCode().ShouldBe(System.Net.HttpStatusCode.NotFound);
        exception.GetErrorMessages().ShouldContain(ResourceMessagesException.PLAYLIST_NOTFOUND);
    }

    private static DeletePlaylistUseCase CreateUseCase(MyMusicLibrary.Domain.Entities.User user, MyMusicLibrary.Domain.Entities.Playlist playlist)
    {
        var loggedUser = LoggedUserBuilder.Build(user);
        var unitOfWork = UnitOfWorkBuilder.Build();
        var repositoryWriteOnly = DeletePlaylistRepositoryBuilder.Build();
        var repositoryReadOnly = new PlaylistReadOnlyRepositoryBuilder();

        if (playlist is not null)
            repositoryReadOnly.GetById(user, playlist);

        return new DeletePlaylistUseCase(loggedUser, unitOfWork, repositoryWriteOnly, repositoryReadOnly.Build());
    }
}
