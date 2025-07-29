using MyMusicLibrary.Domain.Repositories.UnitOfWork;

namespace MyMusicLibrary.Infrastructure.DataAccess;
public class UnitOfWork : IUnitOfWork
{
    private readonly MyMusicLibraryDbContext _dbContext;

    public UnitOfWork(MyMusicLibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Commit()
    {
        await _dbContext.SaveChangesAsync();
    }
}
