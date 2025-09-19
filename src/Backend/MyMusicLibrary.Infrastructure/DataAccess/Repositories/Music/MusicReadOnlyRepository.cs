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
            .AsNoTracking()
            .Where(m => m.UserId == user.Id)
            .Include(m => m.Artist)
            .Take(5)
            .ToListAsync();

    public async Task<IList<(string Genre, int Count)>> GetGenres()
    {
        var genres = await _dbContext.Artist
            .AsNoTracking()
            .GroupBy(a => a.Genre)  
            .Select(g => new { Genre = g.Key, Count = g.Count() })
            .OrderByDescending(g => g.Count)
            .Take(5)
            .ToListAsync();

        return genres.Select(g => (g.Genre, g.Count)).ToList();
    }

    public async Task<Domain.Entities.Music?> GetMusicFavoriteId(Domain.Entities.User user, long favoriteMusicId)
    {
        var favoritedMusic = await _dbContext.UserFavoritesMusic
        .AsNoTracking()
        .Where(u => u.UserId == user.Id && u.Id.Equals(favoriteMusicId))
        .FirstOrDefaultAsync();

        if (favoritedMusic is null)
            return null;

        var musicId = favoritedMusic.MusicId;

        var music = await _dbContext.Music
            .AsNoTracking()
            .Where(m => m.Id == musicId)
            .Include(m => m.Artist)
            .FirstOrDefaultAsync();

        return music;
    }

    public async Task<IList<Domain.Entities.Music>> GetUserMusicFavorites(Domain.Entities.User user)
    {
        var favoriteMusicIds = await _dbContext.UserFavoritesMusic
            .Where(f => f.UserId == user.Id)
            .Select(f => f.MusicId)
            .ToListAsync();

        if (favoriteMusicIds is null)
            return null!;

        var musics = await _dbContext.Music
            .AsNoTracking()
            .Where(m => favoriteMusicIds.Contains(m.Id))
            .Include(m => m.Artist)
            .Take(5)
            .ToListAsync();

        return musics;
    }

    public async Task<IList<Domain.Entities.Music>> Search(Domain.Entities.User user, string name)
    {
        var searchMusic = await _dbContext.Music
            .AsNoTracking()
            .Where(m => m.Name.Contains(name))
            .Include(m => m.Artist)
            .Take(5)
            .ToListAsync(); 

        return searchMusic;
    }

    public async Task<bool> ThereIsThisSong(Domain.Entities.User user, string musicName, string? album) =>
        await _dbContext.Music
            .AnyAsync(m => m.UserId == user.Id && m.Name == musicName && m.Album == album);
}
