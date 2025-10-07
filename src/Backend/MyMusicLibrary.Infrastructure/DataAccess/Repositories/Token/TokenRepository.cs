using Microsoft.EntityFrameworkCore;
using MyMusicLibrary.Domain.Entities;
using MyMusicLibrary.Domain.Repositories.Token;

namespace MyMusicLibrary.Infrastructure.DataAccess.Repositories.Token;
public class TokenRepository : ITokenRepository
{
    private readonly MyMusicLibraryDbContext _dbContext;

    public TokenRepository(MyMusicLibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<RefreshToken?> Get(string refreshToken)
    {
        return await _dbContext
            .RefreshTokens
            .AsNoTracking()
            .Include(token => token.User)
            .FirstOrDefaultAsync(token => token.Value.Equals(refreshToken)); //procurando um refresh token com o valor que recebi
    }

    public async Task SaveNewRefreshToken(RefreshToken refreshToken)
    {
        var tokens = _dbContext.RefreshTokens.Where(t => t.UserId == refreshToken.UserId);

        _dbContext.RefreshTokens.RemoveRange(tokens);

        await _dbContext.RefreshTokens.AddAsync(refreshToken);  
    }
}
