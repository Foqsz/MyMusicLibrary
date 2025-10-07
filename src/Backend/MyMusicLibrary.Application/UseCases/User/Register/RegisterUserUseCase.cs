using AutoMapper;
using MyMusicLibrary.Communication.Request;
using MyMusicLibrary.Communication.Responses;
using MyMusicLibrary.Domain.Repositories.Token;
using MyMusicLibrary.Domain.Repositories.UnitOfWork;
using MyMusicLibrary.Domain.Repositories.User;
using MyMusicLibrary.Domain.Security.Cryptography;
using MyMusicLibrary.Domain.Security.Tokens;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;

namespace MyMusicLibrary.Application.UseCases.User.Register;
public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly ITokenRepository _tokenRepository;

    public RegisterUserUseCase(IUserWriteOnlyRepository userWriteOnlyRepository,
        IUserReadOnlyRepository userReadOnlyRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IPasswordEncripter passwordEncripter,
        IAccessTokenGenerator accessTokenGenerator,
        IRefreshTokenGenerator refreshTokenGenerator,
        ITokenRepository tokenRepository)
    {
        _userWriteOnlyRepository = userWriteOnlyRepository;
        _userReadOnlyRepository = userReadOnlyRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _passwordEncripter = passwordEncripter;
        _accessTokenGenerator = accessTokenGenerator;
        _refreshTokenGenerator = refreshTokenGenerator;
        _tokenRepository = tokenRepository;
    }

    public async Task<ResponseRegisteredUserJson> Execute (RequestRegisterUserJson request)
    {
        await Validate(request);

        var user = _mapper.Map<Domain.Entities.User>(request);
        user.Password = _passwordEncripter.Encrypt(request.Password);
        user.UserIdentifier = Guid.NewGuid();   

        await _userWriteOnlyRepository.Add(user);

        await _unitOfWork.Commit();

        var refreshToken = await CreateAndSaveRefreshToken(user);

        return new ResponseRegisteredUserJson
        {
            Name = request.Name,
            Tokens = new ResponseTokensJson
            {
                AccessToken = _accessTokenGenerator.Generate(user.UserIdentifier),
                RefreshToken = refreshToken
            }
        };
    } 

    private async Task Validate(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();

        var result = await validator.ValidateAsync(request);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages); 
        }
        
        var emailExist = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);

        if (emailExist)
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesException.EMAIL_ALREADY_REGISTERED));

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
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
