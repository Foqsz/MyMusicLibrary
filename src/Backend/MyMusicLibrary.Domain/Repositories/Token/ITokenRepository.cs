using MyMusicLibrary.Domain.Entities;

namespace MyMusicLibrary.Domain.Repositories.Token;
public interface ITokenRepository
{
    Task<RefreshToken?> Get(string refreshToken);
    Task SaveNewRefreshToken(RefreshToken refreshToken);
}
