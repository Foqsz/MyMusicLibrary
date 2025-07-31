using Microsoft.EntityFrameworkCore;
using MyMusicLibrary.Domain.Entities;
using MyMusicLibrary.Domain.Repositories.User;

namespace MyMusicLibrary.Infrastructure.DataAccess.Repositories;
public class UserReadOnlyRepository : IUserReadOnlyRepository
{
    private readonly MyMusicLibraryDbContext _dbContext;

    public UserReadOnlyRepository(MyMusicLibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> ExistActiveUserWithEmail(string email) => await _dbContext.Users.AnyAsync(user => user.Active && user.Email.Equals(email) && user.Active);

    public async Task<bool> ExistActiveUserWithIdentifier(Guid userIdentifier) => await _dbContext.Users.AnyAsync(user => user.Active && user.UserIdentifier.Equals(userIdentifier));

    public async Task<User?> GetByEmail(string email)
    {
        return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Active && user.Email.Equals(email));
    }

    public async Task<User> GetById(long id) => await _dbContext.Users.FirstAsync(user => user.Id == id);
}
