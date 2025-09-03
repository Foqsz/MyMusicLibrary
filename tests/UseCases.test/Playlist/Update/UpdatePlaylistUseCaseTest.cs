using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositores;
using CommonTestUtilities.Requests;
using MyMusicLibrary.Application.UseCases.Playlist.Update;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;
using Shouldly;
using Xunit;

namespace UseCases.Test.Playlist.Update;
public class UpdatePlaylistUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var user, var _) = UserBuilder.Build();
        var playlist = PlaylistBuilder.Build(user);

        var useCase = CreateUseCase(user, playlist);

        var newPlaylist = RequestFromPlaylistJsonBuilder.Build();

        var result = await useCase.Execute(playlist.Id, newPlaylist);

        result.ShouldNotBeNull();
        result.Name.ShouldBe(newPlaylist.Name);
        result.Description.ShouldBe(newPlaylist.Description);
    }

    [Fact]
    public async Task Error_Dados_No_Changes()
    {
        (var user, var _) = UserBuilder.Build();
        var playlist = PlaylistBuilder.Build(user);

        var useCase = CreateUseCase(user, playlist);

        var newPlaylist = RequestFromPlaylistJsonBuilder.Build();
        newPlaylist.Name = playlist.Name;
        newPlaylist.Description = playlist.Description;

        var exception = await Should.ThrowAsync<InvalidUpdateException>(async () => await useCase.Execute(playlist.Id, newPlaylist));

        exception.GetStatusCode().ShouldBe(System.Net.HttpStatusCode.BadRequest);
        exception.GetErrorMessages().ShouldContain(ResourceMessagesException.UPDATE_ERROR);
    }

    [Fact]
    public async Task Error_Playlist_NotFound()
    {
        (var user, var _) = UserBuilder.Build();
        var playlist = PlaylistBuilder.Build(user);

        var useCase = CreateUseCase(user, playlist);

        var newPlaylist = RequestFromPlaylistJsonBuilder.Build();

        var exception = await Should.ThrowAsync<PlaylistException>(async () => await useCase.Execute(2, newPlaylist));

        exception.GetStatusCode().ShouldBe(System.Net.HttpStatusCode.NotFound);
        exception.GetErrorMessages().ShouldContain(ResourceMessagesException.PLAYLIST_NOTFOUND);
    }

    private static UpdatePlaylistUseCase CreateUseCase(MyMusicLibrary.Domain.Entities.User user, 
        MyMusicLibrary.Domain.Entities.Playlist playlist)
    {
        var loggedUser = LoggedUserBuilder.Build(user);
        var unitOfWork = UnitOfWorkBuilder.Build();
        var repositoryReadOnly = new PlaylistReadOnlyRepositoryBuilder();
        var repositoryWriteOnly = PlaylistWriteOnlyRepositoryBuilder.Build();

        if(playlist is not null)
            repositoryReadOnly.GetById(user, playlist);

        return new UpdatePlaylistUseCase(loggedUser, unitOfWork, repositoryReadOnly.Build(), repositoryWriteOnly);
    }
}
