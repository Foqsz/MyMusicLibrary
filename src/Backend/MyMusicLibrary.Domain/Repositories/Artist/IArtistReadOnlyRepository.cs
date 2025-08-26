namespace MyMusicLibrary.Domain.Repositories.Artist;
public interface IArtistReadOnlyRepository
{
    Task<IList<Entities.Artist>> SearchArtist(Entities.User user, string name);
}
