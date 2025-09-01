using Moq;
using MyMusicLibrary.Domain.Repositories.Playlist;

namespace CommonTestUtilities.Repositores;
public class DeletePlaylistRepositoryBuilder
{
    public static IDeletePlaylistRepository Build()
    {
        var mock = new Mock<IDeletePlaylistRepository>();
        return mock.Object;
    }
}
