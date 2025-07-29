using MyMusicLibrary.Domain.Entities;
using MyMusicLibrary.Domain.Repositories.UnitOfWork;
using MyMusicLibrary.Domain.Repositories.User;

namespace MyMusicLibrary.Infrastructure.DataAccess.Repositories;
public class UserWriteOnlyRepository : IUserWriteOnlyRepository
{
    private readonly MyMusicLibraryDbContext _dbContext; 

    public UserWriteOnlyRepository(MyMusicLibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(User user) => await _dbContext.Users.AddAsync(user);
}
