using Moq;
using MyMusicLibrary.Domain.Repositories.User.Update;

namespace CommonTestUtilities.Repositores;
public class UserUpdateWriteOnlyRepositoryBuilder
{
    public static IUpdateUserRepository Build()
    {
        var mock = new Mock<IUpdateUserRepository>();

        return mock.Object;
    }
}
