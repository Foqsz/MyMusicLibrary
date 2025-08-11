using CommonTestUtilities.Cryptografhy;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositores;
using CommonTestUtilities.Tokens.Generator;
using MyMusicLibrary.Application.UseCases.User.DoLogin;
using MyMusicLibrary.Communication.Request;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;
using Shouldly;
using Xunit;

namespace UseCases.Test.User.DoLogin;
public class DoLoginUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var user, var password) = UserBuilder.Build(); 

        var useCase = CreateUseCase(user);

        var result = await useCase.Execute(new RequestDoLoginJson
        {
            Email = user.Email,
            Password = password
        });

        result.ShouldNotBeNull(); 
        result.Tokens.ShouldNotBeNull();
        result.Name.ShouldNotBeNullOrWhiteSpace(user.Name);
        result.Tokens.AccessToken.ShouldNotBeNullOrEmpty();
    }

    [Fact]
    public async Task Error_Invalid_Email()
    {
        (var user, var password) = UserBuilder.Build();
        var useCase = CreateUseCase(user);

        var result = new RequestDoLoginJson
        {
            Email = "invalid-email",
            Password = password
        }; 

        Func<Task> act = async () => await useCase.Execute(result);

        var exception = await act.ShouldThrowAsync<InvalidLoginException>();

        exception.GetErrorMessages().Count.ShouldBe(1);

        exception.GetErrorMessages().First().ShouldBe(ResourceMessagesException.LOGIN_INVALID);
    }

    [Fact]
    public async Task Error_Invalid_Password()
    {
        (var user, var password) = UserBuilder.Build();
        var useCase = CreateUseCase(user);

        var result = new RequestDoLoginJson
        {
            Email = user.Email,
            Password = "123"
        };

        Func<Task> act = async () => await useCase.Execute(result);

        var exception = await act.ShouldThrowAsync<InvalidLoginException>();

        exception.GetErrorMessages().Count.ShouldBe(1);

        exception.GetErrorMessages().First().ShouldBe(ResourceMessagesException.LOGIN_INVALID);
    }

    private static DoLoginUseCase CreateUseCase(MyMusicLibrary.Domain.Entities.User? user = null)
    {
        var repository = new UserReadOnlyRepositoryBuilder();
        var passwordEncripter = PasswordEncripterBuilder.Build();
        var accessTokenGenerator = JwtTokenGeneratorBuilder.Build();

        if (user is not null)
            repository.GetByEmail(user);

        return new DoLoginUseCase(
            userReadOnlyRepository: repository.Build(),
            passwordEncripter: passwordEncripter,
            acessTokenGenerator: accessTokenGenerator
        );
    }
}
