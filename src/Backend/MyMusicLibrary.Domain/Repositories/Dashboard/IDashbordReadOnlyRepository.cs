namespace MyMusicLibrary.Domain.Repositories.Dashboard;
public interface IDashbordReadOnlyRepository
{
    Task<IList<Domain.Entities.Music>> GetForDashboard(Domain.Entities.User user);
}
