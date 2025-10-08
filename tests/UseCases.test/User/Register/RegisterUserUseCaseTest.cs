using CommonTestUtilities.Cryptografhy;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositores;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens.Generator;
using CommonTestUtilities.Tokens.Refresh;
using MyMusicLibrary.Application.UseCases.User.Register;
using MyMusicLibrary.Domain.Extensions;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;
using Shouldly;
using Xunit;

namespace UseCases.Test.User.Register;
public class RegisterUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserJsonBuilder.Build(); 

        var user = new MyMusicLibrary.Domain.Entities.User  
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password,
            UserIdentifier = Guid.NewGuid()
        };

        var useCase = CreateUseCase(user, isNull: false);

        var result = await useCase.Execute(request);

        result.ShouldNotBeNull(); 
        result.Tokens.ShouldNotBeNull();
        result.Tokens.AccessToken.ShouldNotBeNullOrEmpty();
        result.Name.ShouldBe(request.Name);
        result.Tokens.RefreshToken.ShouldNotBeNullOrEmpty();
    }

    [Fact]
    public async Task Error_User_Name_Empty()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        request.Name = string.Empty;

        var user = new MyMusicLibrary.Domain.Entities.User
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password,
            UserIdentifier = Guid.NewGuid()
        };

        var useCase = CreateUseCase(user, isNull: false); 

        Func<Task> act = async () => await useCase.Execute(request);

        var exception = await act.ShouldThrowAsync<ErrorOnValidationException>();

        exception.GetErrorMessages().Count.ShouldBe(1);

        exception.GetErrorMessages().First().ShouldBe(ResourceMessagesException.NAME_EMPTY);
    }

    [Fact]
    public async Task Error_User_Email_Exists_Active()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        var user = new MyMusicLibrary.Domain.Entities.User
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password,
            UserIdentifier = Guid.NewGuid()
        };

        var useCase = CreateUseCase(user, isNull: false, user.Email);

        Func<Task> act = async () => await useCase.Execute(request); 

        var exception = await act.ShouldThrowAsync<ErrorOnValidationException>();
        exception.GetErrorMessages().Count.ShouldBe(1);
        exception.GetErrorMessages().First().ShouldBe(ResourceMessagesException.EMAIL_ALREADY_REGISTERED);
    }

    [Fact]
    public async Task Error_Name_Empty()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        var user = new MyMusicLibrary.Domain.Entities.User
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password,
            UserIdentifier = Guid.NewGuid()
        };

        request.Name = string.Empty;

        var useCase = CreateUseCase(user, isNull: false);

        Func<Task> act = async () => await useCase.Execute(request);

        var exception = await act.ShouldThrowAsync<ErrorOnValidationException>();
        exception.GetErrorMessages().Count.ShouldBe(1);
        exception.GetErrorMessages().First().ShouldBe(ResourceMessagesException.NAME_EMPTY);
    }

    private static RegisterUserUseCase CreateUseCase(MyMusicLibrary.Domain.Entities.User user, bool isNull, string? email = null)
    {
        var repositoryWriteOnly = UserWriteOnlyRepositoryBuilder.Build();
        var repositoryReadOnly = new UserReadOnlyRepositoryBuilder();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var passwordEncripter = PasswordEncripterBuilder.Build();
        var tokenRepository = new TokenRepositoryBuilder();
        var mapper = MapperBuilder.Build();
        var tokenGenerator = JwtTokenGeneratorBuilder.Build();  
        var refreshTokenGenerator = RefreshTokenGeneratorBuilder.Build();

        if (email is not null && isNull.IsFalse())
        {
            repositoryReadOnly.ExistActiveUserWithEmail(email);
            tokenRepository.Get(user, "123aaxx");
            tokenRepository.SaveNewRefreshToken(user);
        }


        return new RegisterUserUseCase(
            repositoryWriteOnly,
            repositoryReadOnly.Build(),
            unitOfWork,
            mapper,
            passwordEncripter,
            tokenGenerator,
            refreshTokenGenerator,
            tokenRepository.Build()
        );
    }
}
