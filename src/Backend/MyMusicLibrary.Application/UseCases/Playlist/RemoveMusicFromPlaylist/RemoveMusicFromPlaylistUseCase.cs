using MyMusicLibrary.Communication.Request;
using MyMusicLibrary.Domain.Extensions;
using MyMusicLibrary.Domain.Repositories.Music;
using MyMusicLibrary.Domain.Repositories.Playlist;
using MyMusicLibrary.Domain.Repositories.UnitOfWork;
using MyMusicLibrary.Domain.Services.LoggedUser;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;

namespace MyMusicLibrary.Application.UseCases.Playlist.RemoveMusicFromPlaylist;
public class RemoveMusicFromPlaylistUseCase : IRemoveMusicFromPlaylistUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IMusicReadOnlyRepository _musicReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveMusicFromPlaylistUseCase(ILoggedUser loggedUser,
        IUnitOfWork unitOfWork,
        IMusicReadOnlyRepository musicReadOnlyRepository)
    {
        _loggedUser = loggedUser;
        _unitOfWork = unitOfWork;
        _musicReadOnlyRepository = musicReadOnlyRepository;
    }

    public async Task Execute(RequestMusicPlaylistJson request)
    {
        var user = await _loggedUser.User();

        var music = await _musicReadOnlyRepository.GetById(user, request.MusicId) ??
            throw new ExistMusicException(ResourceMessagesException.MUSIC_EMPTY);

        if (music.PlaylistId.Equals(request.PlaylistId).IsFalse())
            throw new PlaylistException(ResourceMessagesException.ERROR_MUSIC_FROM_PLAYLIST);

        music.PlaylistId = null;
        await _unitOfWork.Commit();
    }
}
