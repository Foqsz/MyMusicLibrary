namespace MyMusicLibrary.Domain.Repositories.User.Delete;
public interface IUserDeleteAccountRepository
{
    Task DeleteAccount(Guid userIdentifier);
}
