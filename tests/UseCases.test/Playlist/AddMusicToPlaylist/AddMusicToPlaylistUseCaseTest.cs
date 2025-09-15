using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositores;
using CommonTestUtilities.Requests;
using MyMusicLibrary.Application.UseCases.Playlist.AddMusicToPlaylist;
using MyMusicLibrary.Domain.Extensions;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;
using Shouldly;
using Xunit;

namespace UseCases.Test.Playlist.AddMusicToPlaylist;
public class AddMusicToPlaylistUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var user, var _) = UserBuilder.Build();
        var music = MusicBuilder.Build(user);
        var playlist = PlaylistBuilder.Build(user);

        var request = RequestMusicPlaylistJsonBuilder.Build();
        request.MusicId = music.Id;
        request.PlaylistId = playlist.Id;

        var useCase = CreateUseCase(user, playlist, music, false);

        var result = await useCase.Execute(request);

        result.ShouldNotBeNull();
        result.Music.ShouldNotBeNull();
        result.Playlist.ShouldNotBeNull();
    }

    [Fact]
    public async Task Error_MusicNotFound()
    {
        (var user, var _) = UserBuilder.Build();
        var music = MusicBuilder.Build(user);
        var playlist = PlaylistBuilder.Build(user);

        var request = RequestMusicPlaylistJsonBuilder.Build();
        request.MusicId = 1;
        request.PlaylistId = playlist.Id;

        var useCase = CreateUseCase(user, playlist, music, false);

        var exception = await Should.ThrowAsync<ExistMusicException>(async () => await useCase.Execute(request));

        exception.GetStatusCode().ShouldBe(System.Net.HttpStatusCode.BadRequest);
        exception.GetErrorMessages().ShouldContain(ResourceMessagesException.MUSIC_EMPTY);
    }

    [Fact]
    public async Task Error_PlaylistNotFound()
    {
        (var user, var _) = UserBuilder.Build();
        var music = MusicBuilder.Build(user);
        var playlist = PlaylistBuilder.Build(user);

        var request = RequestMusicPlaylistJsonBuilder.Build();
        request.MusicId = music.Id;
        request.PlaylistId = 1;

        var useCase = CreateUseCase(user, playlist, music, false);

        var exception = await Should.ThrowAsync<PlaylistException>(async () => await useCase.Execute(request));

        exception.GetStatusCode().ShouldBe(System.Net.HttpStatusCode.NotFound);
        exception.GetErrorMessages().ShouldContain(ResourceMessagesException.PLAYLIST_NOTFOUND);
    }

    private static AddMusicToPlaylistUseCase CreateUseCase(MyMusicLibrary.Domain.Entities.User user, 
        MyMusicLibrary.Domain.Entities.Playlist playlist,
        MyMusicLibrary.Domain.Entities.Music music,
        bool notfound)
    {
        var repositoryPlaylist = new PlaylistReadOnlyRepositoryBuilder();
        var loggedUser = LoggedUserBuilder.Build(user);
        var repositoryMusic = new MusicReadOnlyRepositoryBuilder();
        var unitOfWork = UnitOfWorkBuilder.Build();

        if (notfound.IsFalse())
        {
            repositoryPlaylist.GetById(user, playlist);
            repositoryMusic.GetById(user, music);
        }

        return new AddMusicToPlaylistUseCase(repositoryPlaylist.Build(), loggedUser, repositoryMusic.Build(), unitOfWork);
    }
}
