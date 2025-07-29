namespace MyMusicLibrary.Domain.Repositories.UnitOfWork;
public interface IUnitOfWork
{
    Task Commit();
}
