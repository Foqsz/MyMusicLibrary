using Microsoft.EntityFrameworkCore;
using MyMusicLibrary.Domain.Entities;
using MyMusicLibrary.Domain.Extensions;
using MyMusicLibrary.Domain.Repositories.Playlist;

namespace MyMusicLibrary.Infrastructure.DataAccess.Repositories.Playlist;
public class PlaylistReadOnlyRepository : IPlaylistReadOnlyRepository
{
    private readonly MyMusicLibraryDbContext _dbContext;

    public PlaylistReadOnlyRepository(MyMusicLibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IList<Domain.Entities.Playlist>> GetAll(Domain.Entities.User user) =>
        await _dbContext.Playlist.AsNoTracking().Where(p => p.UserId == user.Id).Take(5).ToListAsync();

    public async Task<Domain.Entities.Playlist?> GetById(Domain.Entities.User user, long playlistId) => 
        await _dbContext.Playlist.Where(p => p.UserId == user.Id && user.Active && p.Id == playlistId).FirstOrDefaultAsync();

    public async Task<IList<Domain.Entities.Playlist>> GetByName(string name) =>
        await _dbContext.Playlist.AsNoTracking().Where(p => p.Name.Contains(name)).Take(5).ToListAsync();

    public async Task<Domain.Entities.Playlist?> GetPlaylistSongs(Domain.Entities.User user, long playlistId)
    {
        var playlist = await _dbContext.Playlist.Where(p => p.UserId == user.Id && user.Active && p.Id == playlistId).FirstOrDefaultAsync();

        if (playlist is null)
            return null;

        var musics = await _dbContext.Music
            .AsNoTracking()
            .Where(m => m.UserId == user.Id && m.PlaylistId == playlistId)
            .ToListAsync();

        playlist.Musics = musics;

        return playlist;
    }
}
