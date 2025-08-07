using Moq;
using MyMusicLibrary.Domain.Entities;
using MyMusicLibrary.Domain.Repositories.Dashboard;

namespace CommonTestUtilities.Repositores;
public class DashboardReadOnlyRepositoryBuilder
{
    private readonly Mock<IDashboardReadOnlyRepository> _repository; 

    public DashboardReadOnlyRepositoryBuilder()
    {
        _repository = new Mock<IDashboardReadOnlyRepository>();  
    }

    public DashboardReadOnlyRepositoryBuilder GetForDashboard(User user, IList<Music> musics)
    {
        _repository.Setup(r => r.GetForDashboard(user)).ReturnsAsync(musics);

        return this;
    }


    public IDashboardReadOnlyRepository Build() => _repository.Object;
}
