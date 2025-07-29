namespace MyMusicLibrary.Domain.Repositories.User;
public interface IUserReadOnlyRepository
{
    Task<Entities.User> GetById(long id);
    Task<Entities.User?> GetByEmail(string email); 
    Task<bool> ExistActiveUserWithEmail(string email);
}
