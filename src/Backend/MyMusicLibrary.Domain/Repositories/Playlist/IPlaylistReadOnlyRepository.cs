namespace MyMusicLibrary.Domain.Repositories.Playlist;
public interface IPlaylistReadOnlyRepository
{
    Task<Entities.Playlist?> GetById(Entities.User user, long playlistId);
    Task<IList<Entities.Playlist>> GetAll(Entities.User user);
    Task<IList<Entities.Playlist>> GetByName(string name);
}
