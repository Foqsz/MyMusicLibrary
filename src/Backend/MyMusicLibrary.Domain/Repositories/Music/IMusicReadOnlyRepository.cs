namespace MyMusicLibrary.Domain.Repositories.Music;
public interface IMusicReadOnlyRepository
{
    Task<Entities.Music?> GetById(Entities.User user, long musicId);
    Task<IList<Entities.Music>> GetForDashboard(Entities.User user);
    Task<bool> ThereIsThisSong(Entities.User user, string musicName, string album);
    Task<IList<Entities.Music>> Search(Entities.User user, string name);
    Task<IList<(string Genre, int Count)>> GetGenres();
    Task<IList<Entities.Music>> GetUserMusicFavorites(Entities.User user);
    Task<Entities.Music?> GetMusicFavoriteId(Entities.User user, long favoriteMusicId);
}
