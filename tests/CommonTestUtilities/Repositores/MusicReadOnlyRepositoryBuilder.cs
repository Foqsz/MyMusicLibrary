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

    public void ThereIsThisSong(User user, string musicName, string album)
    {
        _repository.Setup(r => r.ThereIsThisSong(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);

        _repository.Setup(r => r.ThereIsThisSong(
                It.Is<User>(u => u.Id == user.Id),
                It.IsAny<string>(),
                It.IsAny<string>())).ReturnsAsync(true);
    }

    public MusicReadOnlyRepositoryBuilder Search(User user, Music music)
    {
        _repository.Setup(r => r.Search(user, music.Name)).ReturnsAsync(new List<Music> { music });
        return this;
    }

    public MusicReadOnlyRepositoryBuilder GetGenres()
    {
        var genres = new List<(string Genre, int Count)>
        {
            ("Rock", 10),
            ("Pop", 5),
            ("Jazz", 2)
        };

        _repository.Setup(r => r.GetGenres()).ReturnsAsync(genres);

        return this;
    }

    public MusicReadOnlyRepositoryBuilder GetUserMusicFavorites(User user, IList<Music> music)
    {
        _repository.Setup(u => u.GetUserMusicFavorites(user)).ReturnsAsync(music);

        return this;
    }

    public MusicReadOnlyRepositoryBuilder GetMusicFavoriteId(User user, Music music)
    {
        _repository.Setup(r => r.GetMusicFavoriteId(user, music.Id)).ReturnsAsync(music);

        return this;
    }

    public MusicReadOnlyRepositoryBuilder GetForDashboard(User user, IList<Music> music)
    {
        _repository.Setup(r => r.GetForDashboard(user)).ReturnsAsync(music);

        return this;
    }

    public IMusicReadOnlyRepository Build()
    {
        return _repository.Object;
    }
}
