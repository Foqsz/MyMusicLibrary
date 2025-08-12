using Moq;
using MyMusicLibrary.Domain.Repositories.User.Delete;

namespace CommonTestUtilities.Repositores;
public class UserDeleteAccountRepositoryBuilder
{
    public static IUserDeleteAccountRepository Build()
    {
        var mock = new Mock<IUserDeleteAccountRepository>();

        return mock.Object;
    }
}
