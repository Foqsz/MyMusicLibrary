namespace MyMusicLibrary.Domain.Repositories.Playlist;
public interface IPlaylistWriteOnlyRepository
{
    Task Create(Entities.User user, Entities.Playlist request);
    void Update(Entities.User user, Entities.Playlist playlist);
}
