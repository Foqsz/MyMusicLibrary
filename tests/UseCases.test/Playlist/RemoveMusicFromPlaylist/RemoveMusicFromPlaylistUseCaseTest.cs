using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositores;
using CommonTestUtilities.Requests;
using MyMusicLibrary.Application.UseCases.Playlist.RemoveMusicFromPlaylist;
using MyMusicLibrary.Domain.Extensions;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;
using Shouldly;
using Xunit;

namespace UseCases.Test.Playlist.RemoveMusicFromPlaylist;
public class RemoveMusicFromPlaylistUseCaseTest
{
    [Fact]
    public void Success()
    {
        (var user, var _) = UserBuilder.Build();
        var music = MusicBuilder.Build(user);
        var playlist = PlaylistBuilder.Build(user);

        music.PlaylistId = playlist.Id;

        var caseSucessoMusimFromPlaylis = RequestMusicPlaylistJsonBuilder.Build();
        caseSucessoMusimFromPlaylis.MusicId = music.Id;
        caseSucessoMusimFromPlaylis.PlaylistId = playlist.Id;

        var useCase = CreateUseCase(user, music, notfound: false);

        var result =  useCase.Execute(caseSucessoMusimFromPlaylis.MusicId, caseSucessoMusimFromPlaylis.PlaylistId);

        result.Status.ShouldBe(TaskStatus.RanToCompletion);
        result.ShouldNotBeNull();
        result.ShouldNotThrow();
    }

    [Fact]
    public async Task Error_Music_NotFound()
    {
        (var user, var _) = UserBuilder.Build();
        var music = MusicBuilder.Build(user);
        var playlist = PlaylistBuilder.Build(user);

        music.PlaylistId = playlist.Id;

        var caseSucessoMusimFromPlaylis = RequestMusicPlaylistJsonBuilder.Build();
        caseSucessoMusimFromPlaylis.MusicId = music.Id;
        caseSucessoMusimFromPlaylis.PlaylistId = playlist.Id;

        var useCase = CreateUseCase(user, music, notfound: true);

        var exception = await Should.ThrowAsync<ExistMusicException>(async () => 
            await useCase.Execute(caseSucessoMusimFromPlaylis.MusicId, caseSucessoMusimFromPlaylis.PlaylistId));

        exception.Message.ShouldBe(ResourceMessagesException.MUSIC_EMPTY);
    }

    [Fact]
    public async Task Error_Playlist_Invalid()
    {
        (var user, var _) = UserBuilder.Build();
        var music = MusicBuilder.Build(user);
        var playlist = PlaylistBuilder.Build(user);

        music.PlaylistId = 123;

        var caseSucessoMusimFromPlaylis = RequestMusicPlaylistJsonBuilder.Build();
        caseSucessoMusimFromPlaylis.MusicId = music.Id;
        caseSucessoMusimFromPlaylis.PlaylistId = playlist.Id;

        var useCase = CreateUseCase(user, music, notfound: false);

        var exception = await Should.ThrowAsync<PlaylistException>(async () => 
            await useCase.Execute(caseSucessoMusimFromPlaylis.MusicId, caseSucessoMusimFromPlaylis.PlaylistId));

        exception.Message.ShouldBe(ResourceMessagesException.ERROR_MUSIC_FROM_PLAYLIST);
    }

    private static RemoveMusicFromPlaylistUseCase CreateUseCase(MyMusicLibrary.Domain.Entities.User user,
        MyMusicLibrary.Domain.Entities.Music music,
        bool notfound)
    {
        var loggedUser = LoggedUserBuilder.Build(user);
        var musicRepository = new MusicReadOnlyRepositoryBuilder();
        var unitOfWork = UnitOfWorkBuilder.Build();

        if (music is not null && notfound.IsFalse())
            musicRepository.GetById(user, music);

        return new RemoveMusicFromPlaylistUseCase(loggedUser, unitOfWork, musicRepository.Build());
    }
}
