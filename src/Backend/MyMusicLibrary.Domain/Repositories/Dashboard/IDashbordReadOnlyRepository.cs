namespace MyMusicLibrary.Domain.Repositories.Dashboard;
public interface IDashboardReadOnlyRepository
{
    Task<IList<Domain.Entities.Music>> GetForDashboard(Domain.Entities.User user);
}
