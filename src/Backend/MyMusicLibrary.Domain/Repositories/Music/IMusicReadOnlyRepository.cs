namespace MyMusicLibrary.Domain.Repositories.Music;
public interface IMusicReadOnlyRepository
{
    Task<Entities.Music?> GetById(Entities.User user, long musicId);
    Task<IList<Entities.Music>> GetForDashboard(Entities.User user);
    Task<bool> ThereIsThisSong(Entities.User user, string musicName, string album);
}
