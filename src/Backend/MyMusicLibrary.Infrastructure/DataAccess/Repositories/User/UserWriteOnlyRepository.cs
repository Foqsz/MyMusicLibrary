using Microsoft.EntityFrameworkCore;
using MyMusicLibrary.Domain.Repositories.User;

namespace MyMusicLibrary.Infrastructure.DataAccess.Repositories.User;
public class UserWriteOnlyRepository : IUserWriteOnlyRepository
{
    private readonly MyMusicLibraryDbContext _dbContext;

    public UserWriteOnlyRepository(MyMusicLibraryDbContext dbContext) => _dbContext = dbContext;

    public async Task Add(Domain.Entities.User user) => await _dbContext.Users.AddAsync(user);

    public void Update(Domain.Entities.User user) => _dbContext.Users.Update(user);

    public async Task DeleteAccount(Guid userIdentifier)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.UserIdentifier == userIdentifier);

        if (user is null)
            return;

        var musics = await _dbContext.Music.Where(music => music.UserId == user.Id).ToListAsync();

        _dbContext.Users.Remove(user); 

        if(!musics.Any())
            return;

        _dbContext.Music.RemoveRange(musics); 
    }
}
