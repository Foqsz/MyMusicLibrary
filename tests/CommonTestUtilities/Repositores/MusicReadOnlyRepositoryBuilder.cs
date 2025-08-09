using Moq;
using MyMusicLibrary.Domain.Entities;
using MyMusicLibrary.Domain.Repositories.Music;

namespace CommonTestUtilities.Repositores;
public class MusicReadOnlyRepositoryBuilder
{
    private readonly Mock<IMusicReadOnlyRepository> _repository;

    public MusicReadOnlyRepositoryBuilder() => _repository = new Mock<IMusicReadOnlyRepository>();

    public MusicReadOnlyRepositoryBuilder GetById(User user, Music? music)
    {
        if (music is not null)
        {
            _repository.Setup(repository => repository.GetById(user, music.Id))
                .ReturnsAsync(music);

        }
        return this;
    }

    public IMusicReadOnlyRepository Build()
    {
        return _repository.Object;
    }
}
