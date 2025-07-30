using Moq;
using MyMusicLibrary.Domain.Repositories.User;

namespace CommonTestUtilities.Repositores;
public class UserWriteOnlyRepositoryBuilder
{
    public static IUserWriteOnlyRepository Build()
    {
        var mock = new Mock<IUserWriteOnlyRepository>();

        return mock.Object;
    }
}
