using Microsoft.EntityFrameworkCore;
using MyMusicLibrary.Domain.Repositories.User;
using MyMusicLibrary.Domain.Repositories.User.Delete;
using MyMusicLibrary.Domain.Repositories.User.Update;

namespace MyMusicLibrary.Infrastructure.DataAccess.Repositories.User;
public class UserWriteOnlyRepository : IUserWriteOnlyRepository, IUserDeleteAccountRepository, IUpdateUserRepository
{
    private readonly MyMusicLibraryDbContext _dbContext;

    public UserWriteOnlyRepository(MyMusicLibraryDbContext dbContext) => _dbContext = dbContext;

    public async Task Add(Domain.Entities.User user) => await _dbContext.Users.AddAsync(user);

    public async Task DeleteAccount(Guid userIdentifier)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.UserIdentifier == userIdentifier);

        if (user is null)
            return;

        var musics = await _dbContext.Music.Where(music => music.UserId == user.Id).ToListAsync();

        _dbContext.Users.Remove(user); 

        if(musics.Count == 0)
            return;

        _dbContext.Music.RemoveRange(musics); 
    }

    public void Update(Domain.Entities.User user) => _dbContext.Users.Update(user);
}
