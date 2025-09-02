using Microsoft.EntityFrameworkCore;
using MyMusicLibrary.Domain.Repositories.Playlist;

namespace MyMusicLibrary.Infrastructure.DataAccess.Repositories.Playlist;
public class PlaylistWriteOnlyRepository : IPlaylistWriteOnlyRepository, IDeletePlaylistRepository
{
    private readonly MyMusicLibraryDbContext _dbContext;

    public PlaylistWriteOnlyRepository(MyMusicLibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create(Domain.Entities.User user, Domain.Entities.Playlist request) => await _dbContext.Playlist.AddAsync(request);

    public async Task Delete(Domain.Entities.User user, long playlistId)
    {
        var playlist = await _dbContext.Playlist
            .Where(p => p.UserId == user.Id && user.Active && p.Id == playlistId)
            .FirstOrDefaultAsync();

        if (playlist is null)
            return;

        _dbContext.Playlist.Remove(playlist);
    }

    public void Update(Domain.Entities.User user, Domain.Entities.Playlist playlist)
    {
        _dbContext.Playlist.Update(playlist); 
    }
}
