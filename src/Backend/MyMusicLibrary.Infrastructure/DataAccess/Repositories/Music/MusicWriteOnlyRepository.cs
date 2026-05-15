using Microsoft.EntityFrameworkCore;
using MyMusicLibrary.Domain.Repositories.Music;

namespace MyMusicLibrary.Infrastructure.DataAccess.Repositories.Music;
public class MusicWriteOnlyRepository : IMusicWriteOnlyRepository
{
    private readonly MyMusicLibraryDbContext _dbContext;

    public MusicWriteOnlyRepository(MyMusicLibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Domain.Entities.Music music) => await _dbContext.Music.AddAsync(music);

    public async Task AddMusicFavorite(Domain.Entities.UserFavoritesMusic musicFavorite) =>
        await _dbContext.UserFavoritesMusic.AddAsync(musicFavorite);

    public async Task Delete(long musicId)
    {
        var music = await _dbContext.Music.FindAsync(musicId);
        var musicFavorite = await _dbContext.UserFavoritesMusic.Where(m => m.UserId == music!.UserId && m.MusicId == music.Id).FirstOrDefaultAsync();
        var artist = await _dbContext.Artist.Where(a => a.MusicId == music!.Id).ToListAsync();

        if (musicFavorite is not null)
            _dbContext.UserFavoritesMusic.Remove(musicFavorite);

        if (artist is not null && artist.Count > 0)
            _dbContext.Artist.RemoveRange(artist);

        _dbContext.Music.Remove(music!);
    }

    public async Task UnfavoriteMusic(long musicId)
    {
        var music = await _dbContext.UserFavoritesMusic.FindAsync(musicId);

        _dbContext.UserFavoritesMusic.Remove(music!);
    }

    public async Task Update(Domain.Entities.User user, Domain.Entities.Music music)
    {
        var musicUpdate = await _dbContext.Music
            .Where(m => m.Id == music.Id && m.UserId == user.Id)
            .FirstOrDefaultAsync();

        if (musicUpdate is null)
            return;

        _dbContext.Music.Update(musicUpdate);
    }
}
