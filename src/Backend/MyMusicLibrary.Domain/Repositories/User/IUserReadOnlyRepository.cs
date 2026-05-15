namespace MyMusicLibrary.Domain.Repositories.User;
public interface IUserReadOnlyRepository
{
    Task<IList<Entities.User>> GetByIds(IEnumerable<long> ids);
    Task<Entities.User> GetById(long id);
    Task<Entities.User?> GetByEmail(string email); 
    Task<bool> ExistActiveUserWithEmail(string email);
    Task<bool> ExistActiveUserWithIdentifier(Guid userIdentifier);
}
