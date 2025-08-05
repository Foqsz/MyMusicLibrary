using Microsoft.EntityFrameworkCore;
using MyMusicLibrary.Domain.Entities;
using MyMusicLibrary.Domain.Security.Tokens;
using MyMusicLibrary.Domain.Services.LoggedUser;
using MyMusicLibrary.Infrastructure.DataAccess;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MyMusicLibrary.Infrastructure.Services.LoggedUser;
public class LoggedUser : ILoggedUser
{
    private readonly MyMusicLibraryDbContext _dbContext;
    private readonly ITokenProvider _token;

    public LoggedUser(MyMusicLibraryDbContext dbContext, ITokenProvider token)
    {
        _dbContext = dbContext;
        _token = token;
    }

    public async Task<User> User()
    {
        var token = _token.Value();

        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

        var identifier = jwtSecurityToken.Claims.First(c => c.Type == ClaimTypes.Sid).Value;

        var userIdentifier = Guid.Parse(identifier);

        return await _dbContext
            .Users
            .AsNoTracking()
            .FirstAsync(user => user.Active && user.UserIdentifier == userIdentifier);
    }
}
