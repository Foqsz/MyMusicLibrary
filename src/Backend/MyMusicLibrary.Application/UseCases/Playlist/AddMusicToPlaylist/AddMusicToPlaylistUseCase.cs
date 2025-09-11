using MyMusicLibrary.Communication.Request;
using MyMusicLibrary.Communication.Responses;
using MyMusicLibrary.Domain.Repositories.Music;
using MyMusicLibrary.Domain.Repositories.Playlist;
using MyMusicLibrary.Domain.Repositories.UnitOfWork;
using MyMusicLibrary.Domain.Services.LoggedUser;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;

namespace MyMusicLibrary.Application.UseCases.Playlist.AddMusicToPlaylist;
public class AddMusicToPlaylistUseCase : IAddMusicToPlaylistUseCase
{
    private readonly IPlaylistReadOnlyRepository _playlistReadOnlyRepository;
    private readonly ILoggedUser _loggedUser;
    private readonly IMusicReadOnlyRepository _musicReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddMusicToPlaylistUseCase(IPlaylistReadOnlyRepository playlistReadOnlyRepository,
        ILoggedUser loggedUser,
        IMusicReadOnlyRepository musicReadOnlyRepository,
        IUnitOfWork unitOfWork)
    {
        _playlistReadOnlyRepository = playlistReadOnlyRepository;
        _loggedUser = loggedUser;
        _musicReadOnlyRepository = musicReadOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseMusicPlaylistJson> Execute(RequestMusicPlaylistJson request)
    {
        var user = await _loggedUser.User();

        var playlist = await _playlistReadOnlyRepository.GetById(user, request.PlaylistId);

        if (playlist is null)
            throw new PlaylistException(ResourceMessagesException.PLAYLIST_NOTFOUND);

        var music = await _musicReadOnlyRepository.GetById(user, request.MusicId);

        if (music is null)
            throw new ExistMusicException(ResourceMessagesException.MUSIC_EMPTY);

        music.PlaylistId = playlist.Id;
        playlist.Musics.Add(music);
        await _unitOfWork.Commit();

        return new ResponseMusicPlaylistJson()
        {
            Music = music.Name,
            Playlist = playlist.Name
        };
    }
}
