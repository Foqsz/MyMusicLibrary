using Microsoft.EntityFrameworkCore;
using MyMusicLibrary.Domain.Repositories.Dashboard;

namespace MyMusicLibrary.Infrastructure.DataAccess.Repositories.Dashboard;
public class DashbordReadOnlyRepository : IDashbordReadOnlyRepository
{
    private readonly MyMusicLibraryDbContext _dbContext;
    public DashbordReadOnlyRepository(MyMusicLibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IList<Domain.Entities.Music>> GetForDashboard(Domain.Entities.User user)
    {
        return await _dbContext
            .Music
            .AsNoTracking()
            .Include(m => m.Artist)
            .Where(m => m.Active && m.UserId == user.Id)
            .OrderByDescending(m => m.CreatedOn)
            .Take(5)
            .ToListAsync();
    } 
}
