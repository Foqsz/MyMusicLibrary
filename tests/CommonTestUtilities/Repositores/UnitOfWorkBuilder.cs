using Moq;
using MyMusicLibrary.Domain.Repositories.UnitOfWork;

namespace CommonTestUtilities.Repositores;
public class UnitOfWorkBuilder
{
    public static IUnitOfWork Build()
    {
        var mock = new Mock<IUnitOfWork>();
        return mock.Object;
    }
}
