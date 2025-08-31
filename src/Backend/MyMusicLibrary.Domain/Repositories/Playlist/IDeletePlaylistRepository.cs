namespace MyMusicLibrary.Domain.Repositories.Playlist;
public interface IDeletePlaylistRepository
{
    Task Delete(Entities.User user, long playlistId);
}
