namespace MyMusicLibrary.Domain.Repositories.Playlist;
public interface IPlaylistReadOnlyRepository
{
    Task<Entities.Playlist?> GetById(Entities.User user, long playlistId);
}
