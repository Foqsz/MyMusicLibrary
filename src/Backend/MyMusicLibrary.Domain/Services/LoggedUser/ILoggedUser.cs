using MyMusicLibrary.Domain.Entities;

namespace MyMusicLibrary.Domain.Services.LoggedUser;
public interface ILoggedUser
{
    Task<User> User();
}
