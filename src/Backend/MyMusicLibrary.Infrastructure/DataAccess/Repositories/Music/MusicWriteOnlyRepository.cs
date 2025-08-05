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
}
