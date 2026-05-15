using MyMusicLibrary.Domain.Repositories.Playlist;

namespace CommonTestUtilities.Repositores;
public class PlaylistWriteOnlyRepositoryBuilder
{
    public static IPlaylistWriteOnlyRepository Build()
    {
        var mock = new Moq.Mock<IPlaylistWriteOnlyRepository>();
        
        return mock.Object;
    }
}

