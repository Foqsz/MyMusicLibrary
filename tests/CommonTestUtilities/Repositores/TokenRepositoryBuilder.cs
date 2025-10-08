using CommonTestUtilities.Entities;
using Moq;
using MyMusicLibrary.Domain.Entities;
using MyMusicLibrary.Domain.Repositories.Token;

namespace CommonTestUtilities.Repositores;
public class TokenRepositoryBuilder
{
    private readonly Mock<ITokenRepository> _repository;

    public TokenRepositoryBuilder()
    {
        _repository = new Mock<ITokenRepository>(); 
    }

    public TokenRepositoryBuilder Get(User user, string refreshToken)
    {
        var token = RefreshTokenBuilder.Build(user);

        _repository.Setup(r => r.Get(refreshToken)).ReturnsAsync(token);
        return this;
    }

    public TokenRepositoryBuilder SaveNewRefreshToken(User user)
    {
        var token = RefreshTokenBuilder.Build(user);

        _repository.Setup(r => r.SaveNewRefreshToken(token)).Returns(Task.CompletedTask);
        return this;
    }

    public ITokenRepository Build() => _repository.Object;
}
