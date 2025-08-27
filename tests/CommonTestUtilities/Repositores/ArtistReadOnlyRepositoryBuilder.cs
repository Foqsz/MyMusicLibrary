using Moq;
using MyMusicLibrary.Domain.Repositories.Artist;

namespace CommonTestUtilities.Repositores;
public class ArtistReadOnlyRepositoryBuilder
{
    private readonly Mock<IArtistReadOnlyRepository> _repository;

    public ArtistReadOnlyRepositoryBuilder()
    {
        _repository = new Mock<IArtistReadOnlyRepository>();
    }

    public ArtistReadOnlyRepositoryBuilder SearchArtist(MyMusicLibrary.Domain.Entities.User user, MyMusicLibrary.Domain.Entities.Artist artist)
    {
        _repository.Setup(r => r.SearchArtist(user, artist.Name))
            .ReturnsAsync(new List<MyMusicLibrary.Domain.Entities.Artist> { artist });

        return this;
    }

    public IArtistReadOnlyRepository Build() => _repository.Object;
}
