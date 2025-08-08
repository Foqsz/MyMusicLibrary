using Microsoft.EntityFrameworkCore;
using MyMusicLibrary.Domain.Repositories.Music;

namespace MyMusicLibrary.Infrastructure.DataAccess.Repositories.Music;
public class MusicReadOnlyRepository : IMusicReadOnlyRepository
{
    private readonly MyMusicLibraryDbContext _dbContext;

    public MusicReadOnlyRepository(MyMusicLibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Domain.Entities.Music?> GetById(Domain.Entities.User user, long musicId)
    {
        var musicInfo = await _dbContext.Music
            .Include(m => m.Artist) // inclui os artistas relacionados
            .Where(m => m.UserId == user.Id && m.Id == musicId)
            .FirstOrDefaultAsync();

        return musicInfo;
    }

    public async Task<IList<Domain.Entities.Music>> GetForDashboard(Domain.Entities.User user) =>
        await _dbContext.Music
            .Where(m => m.UserId == user.Id) 
            .ToListAsync();

    public async Task<bool> ThereIsThisSong(Domain.Entities.User user, string musicName, string album) =>
        await _dbContext.Music
            .AnyAsync(m => m.UserId == user.Id && m.Name == musicName && m.Album == album);
}
