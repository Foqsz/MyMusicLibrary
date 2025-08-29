using Microsoft.EntityFrameworkCore;
using MyMusicLibrary.Domain.Extensions;
using MyMusicLibrary.Domain.Repositories.Playlist;

namespace MyMusicLibrary.Infrastructure.DataAccess.Repositories.Playlist;
public class PlaylistWriteOnlyRepository : IPlaylistWriteOnlyRepository
{
    private readonly MyMusicLibraryDbContext _dbContext;

    public PlaylistWriteOnlyRepository(MyMusicLibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create(Domain.Entities.User user, Domain.Entities.Playlist request) => await _dbContext.Playlist.AddAsync(request);
}
