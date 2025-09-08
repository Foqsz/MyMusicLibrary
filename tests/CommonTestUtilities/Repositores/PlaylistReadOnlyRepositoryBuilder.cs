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

    public PlaylistReadOnlyRepositoryBuilder GetAll(MyMusicLibrary.Domain.Entities.User user, IList<MyMusicLibrary.Domain.Entities.Playlist> playlist)
    {
        _repository.Setup(p => p.GetAll(user)).ReturnsAsync(playlist);

        return this;
    }

    public PlaylistReadOnlyRepositoryBuilder GetByName(IList<MyMusicLibrary.Domain.Entities.Playlist> playlist, string name)
    {
        _repository.Setup(p => p.GetByName(name)).ReturnsAsync(playlist);

        return this;
    }

    public IPlaylistReadOnlyRepository Build() => _repository.Object;
}
