using Moq;
using MyMusicLibrary.Domain.Repositories.Music;

namespace CommonTestUtilities.Repositores;
public class MusicWriteOnlyRepositoryBuilder
{
    public static IMusicWriteOnlyRepository Build()
    {
        var mock = new Mock<IMusicWriteOnlyRepository>();
        return mock.Object;
    }
}
