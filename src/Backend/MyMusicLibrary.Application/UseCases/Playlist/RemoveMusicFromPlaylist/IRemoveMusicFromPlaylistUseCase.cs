using MyMusicLibrary.Communication.Request;

namespace MyMusicLibrary.Application.UseCases.Playlist.RemoveMusicFromPlaylist;
public interface IRemoveMusicFromPlaylistUseCase
{
    Task Execute(long musicId, long playlistId);
}
