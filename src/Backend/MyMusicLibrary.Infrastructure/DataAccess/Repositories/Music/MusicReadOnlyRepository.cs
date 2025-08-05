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

    public async Task<Domain.Entities.Music?> GetById(Domain.Entities.User user, long musicId) =>
        await _dbContext.Music.Where(m => m.UserId == user.Id && m.Id == musicId).FirstOrDefaultAsync();

    public async Task<IList<Domain.Entities.Music>> GetForDashbord(Domain.Entities.User user) =>
        await _dbContext.Music
            .Where(m => m.UserId == user.Id) 
            .ToListAsync();

    public async Task<bool> ThereIsThisSong(Domain.Entities.User user, string musicName, string album) =>
        await _dbContext.Music
            .AnyAsync(m => m.UserId == user.Id && m.Name == musicName && m.Album == album);
}
