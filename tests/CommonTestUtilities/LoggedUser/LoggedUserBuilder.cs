using Moq;
using MyMusicLibrary.Domain.Entities;
using MyMusicLibrary.Domain.Services.LoggedUser;

namespace CommonTestUtilities.LoggedUser;
public class LoggedUserBuilder
{
    public static ILoggedUser Build(User user)
    {
        var mock = new Mock<ILoggedUser>();

        mock.Setup(u => u.User()).ReturnsAsync(user);

        return mock.Object;
    }
}
