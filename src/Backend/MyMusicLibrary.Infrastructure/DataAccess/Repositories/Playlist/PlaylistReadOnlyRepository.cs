using Microsoft.EntityFrameworkCore;
using MyMusicLibrary.Domain.Repositories.Playlist;

namespace MyMusicLibrary.Infrastructure.DataAccess.Repositories.Playlist;
public class PlaylistReadOnlyRepository : IPlaylistReadOnlyRepository
{
    private readonly MyMusicLibraryDbContext _dbContext;

    public PlaylistReadOnlyRepository(MyMusicLibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Domain.Entities.Playlist?> GetById(Domain.Entities.User user, long playlistId) => 
        await _dbContext.Playlist.Where(p => p.UserId == user.Id && user.Active && p.Id == playlistId).FirstOrDefaultAsync();
}
