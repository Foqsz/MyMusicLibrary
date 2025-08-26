using Microsoft.EntityFrameworkCore;
using MyMusicLibrary.Domain.Repositories.Artist;

namespace MyMusicLibrary.Infrastructure.DataAccess.Repositories.Artist;
public class ArtistReadOnlyRepository : IArtistReadOnlyRepository
{
    private readonly MyMusicLibraryDbContext _dbContext;
    public ArtistReadOnlyRepository(MyMusicLibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IList<Domain.Entities.Artist>> SearchArtist(Domain.Entities.User user, string name)
    {
        var artists = await _dbContext.Artist
            .Where(a => a.Active && user.Active && a.Name.Contains(name))
            .Include(m => m.Music)
            .Take(5)
            .ToListAsync();

        return artists;
    }
}
