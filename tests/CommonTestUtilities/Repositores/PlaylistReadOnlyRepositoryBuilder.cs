using Moq;
using MyMusicLibrary.Domain.Repositories.Playlist;

namespace CommonTestUtilities.Repositores;
public class PlaylistReadOnlyRepositoryBuilder
{
    private readonly Mock<IPlaylistReadOnlyRepository> _repository;

    public PlaylistReadOnlyRepositoryBuilder()
    {
        _repository = new Mock <IPlaylistReadOnlyRepository>();
    }

    public PlaylistReadOnlyRepositoryBuilder GetById(MyMusicLibrary.Domain.Entities.User user, MyMusicLibrary.Domain.Entities.Playlist playlistId)
    {
        _repository.Setup(p => p.GetById(user, playlistId.Id))
            .ReturnsAsync(playlistId);

        return this;
    }

    public IPlaylistReadOnlyRepository Build() => _repository.Object;
}
