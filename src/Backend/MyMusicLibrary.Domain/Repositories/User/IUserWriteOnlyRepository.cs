namespace MyMusicLibrary.Domain.Repositories.User;
public interface IUserWriteOnlyRepository
{
    Task Add(Entities.User user);
    Task DeleteAccount(Guid userIdentifier);
}
