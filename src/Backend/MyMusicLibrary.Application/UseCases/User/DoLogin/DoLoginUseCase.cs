using MyMusicLibrary.Communication.Request;
using MyMusicLibrary.Communication.Responses;
using MyMusicLibrary.Domain.Extensions;
using MyMusicLibrary.Domain.Repositories.Token;
using MyMusicLibrary.Domain.Repositories.UnitOfWork;
using MyMusicLibrary.Domain.Repositories.User;
using MyMusicLibrary.Domain.Security.Cryptography;
using MyMusicLibrary.Domain.Security.Tokens;
using MyMusicLibrary.Exceptions.ExceptionsBase;

namespace MyMusicLibrary.Application.UseCases.User.DoLogin;
public class DoLoginUseCase : IDoLoginUseCase
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    private readonly ITokenRepository _tokenRepository;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly IUnitOfWork _unitOfWork;

    public DoLoginUseCase(IUserReadOnlyRepository userReadOnlyRepository,
        IPasswordEncripter passwordEncripter,
        IAccessTokenGenerator acessTokenGenerator,
        ITokenRepository tokenRepository,
        IRefreshTokenGenerator refreshTokenGenerator,
        IUnitOfWork unitOfWork)
    {
        _userReadOnlyRepository = userReadOnlyRepository;
        _passwordEncripter = passwordEncripter;
        _accessTokenGenerator = acessTokenGenerator;
        _tokenRepository = tokenRepository;
        _refreshTokenGenerator = refreshTokenGenerator;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestDoLoginJson request)
    {
        _passwordEncripter.Encrypt(request.Password);

        var user = await _userReadOnlyRepository.GetByEmail(request.Email);

        if(user is null || _passwordEncripter.IsValid(request.Password, user.Password).IsFalse())
            throw new InvalidLoginException();

        var refreshToken = await CreateAndSaveRefreshToken(user);

        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
            Tokens = new ResponseTokensJson
            {
                AccessToken = _accessTokenGenerator.Generate(user.UserIdentifier), 
                RefreshToken = refreshToken
            }
        };
    }

    private async Task<string> CreateAndSaveRefreshToken(Domain.Entities.User user)
    {
        var refreshToken = new Domain.Entities.RefreshToken()
        {
            Value = _refreshTokenGenerator.Generate(),
            UserId = user.Id,
        };

        await _tokenRepository.SaveNewRefreshToken(refreshToken);

        await _unitOfWork.Commit();

        return refreshToken.Value;
    }
}
