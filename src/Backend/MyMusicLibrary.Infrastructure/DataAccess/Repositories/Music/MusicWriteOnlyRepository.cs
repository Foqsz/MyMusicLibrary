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

        _dbContext.Music.Remove(music!);
    }

    public async Task UnfavoriteMusic(long musicId)
    {
        var music = await _dbContext.UserFavoritesMusic.FindAsync(musicId);

        _dbContext.UserFavoritesMusic.Remove(music!);
    }
}
