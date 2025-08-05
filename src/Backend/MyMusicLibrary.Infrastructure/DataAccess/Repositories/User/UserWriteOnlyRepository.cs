using MyMusicLibrary.Domain.Repositories.User;

namespace MyMusicLibrary.Infrastructure.DataAccess.Repositories.User;
public class UserWriteOnlyRepository : IUserWriteOnlyRepository
{
    private readonly MyMusicLibraryDbContext _dbContext; 

    public UserWriteOnlyRepository(MyMusicLibraryDbContext dbContext) => _dbContext = dbContext;

    public async Task Add(Domain.Entities.User user) => await _dbContext.Users.AddAsync(user);

    public void Update(Domain.Entities.User user) => _dbContext.Users.Update(user);

    public void Delete(Domain.Entities.User user) => _dbContext.Users.Remove(user);
}
