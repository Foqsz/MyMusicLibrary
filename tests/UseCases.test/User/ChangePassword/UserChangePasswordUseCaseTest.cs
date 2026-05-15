using CommonTestUtilities.Cryptografhy;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositores;
using CommonTestUtilities.Requests;
using MyMusicLibrary.Application.UseCases.User.ChangePassword;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;
using Shouldly;
using Xunit;

namespace UseCases.Test.User.ChangePassword;
public class UserChangePasswordUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var user, var password) = UserBuilder.Build();

        var userChangePassword = RequestUserChangePasswordBuilder.Build();
        userChangePassword.CurrentPassword = password;

        var useCase = CreateUseCase(user);

        var request = useCase.Execute(userChangePassword);
        
        await request.ShouldNotBeNull(); 
        await request.ShouldNotThrowAsync();
    }

    [Fact]
    public async Task Error_Invalid_Current_Password()
    {
        (var user, var _) = UserBuilder.Build();

        var userChangePassword = RequestUserChangePasswordBuilder.Build(); 

        var useCase = CreateUseCase(user);

        Func<Task> act = async () => await useCase.Execute(userChangePassword);

        var exception = await act.ShouldThrowAsync<UserChangePasswordException>();

        exception.GetErrorMessages().Count.ShouldBe(1);

        exception.GetErrorMessages().First().ShouldBe(ResourceMessagesException.INCORRECT_CURRENT_PASSWORD_ERROR);
    }

    [Fact]
    public async Task Error_Same_Password()
    {
        (var user, var password) = UserBuilder.Build();

        var userChangePassword = RequestUserChangePasswordBuilder.Build();
        userChangePassword.CurrentPassword = password;
        userChangePassword.NewPassword = password;

        var useCase = CreateUseCase(user);

        Func<Task> act = async () => await useCase.Execute(userChangePassword);

        var exception = await act.ShouldThrowAsync<UserChangePasswordException>();

        exception.GetErrorMessages().Count.ShouldBe(1);

        exception.GetErrorMessages().First().ShouldBe(ResourceMessagesException.SAME_PASSWORD_ERROR);
    }

    [Theory]
    [InlineData("1")]
    [InlineData("2")]
    [InlineData("3")]
    [InlineData("4")]
    [InlineData("5")]
    public async Task Error_Invalid_Lenght_Password(string passwordLenght)
    {
        (var user, var password) = UserBuilder.Build();

        var userChangePassword = RequestUserChangePasswordBuilder.Build();
        userChangePassword.CurrentPassword = password;
        userChangePassword.NewPassword = passwordLenght;

        var useCase = CreateUseCase(user);

        Func<Task> act = async () => await useCase.Execute(userChangePassword);

        var exception = await act.ShouldThrowAsync<ErrorOnValidationException>();

        exception.GetErrorMessages().Count.ShouldBe(1);

        exception.GetErrorMessages().First().ShouldBe(ResourceMessagesException.PASSWORD_INVALID);
    }

    [Fact]
    public async Task Error_Empty_Password()
    {
        (var user, var password) = UserBuilder.Build();

        var userChangePassword = RequestUserChangePasswordBuilder.Build();
        userChangePassword.CurrentPassword = password;
        userChangePassword.NewPassword = "";

        var useCase = CreateUseCase(user);

        Func<Task> act = async () => await useCase.Execute(userChangePassword);

        var exception = await act.ShouldThrowAsync<ErrorOnValidationException>();

        exception.GetErrorMessages().Count.ShouldBe(1);

        exception.GetErrorMessages().First().ShouldBe(ResourceMessagesException.PASSWORD_EMPTY);
    }

    private static UserChangePasswordUseCase CreateUseCase(MyMusicLibrary.Domain.Entities.User? user = null)
    {
        var loggedUser = LoggedUserBuilder.Build(user!);
        var unitOfWork = UnitOfWorkBuilder.Build();
        var passwordEncripter = PasswordEncripterBuilder.Build();
        var repositoryReadOnly = new UserReadOnlyRepositoryBuilder();

        if (user is not null)
            repositoryReadOnly.GetById(user);

        return new UserChangePasswordUseCase(loggedUser, unitOfWork, passwordEncripter, repositoryReadOnly.Build());
    }
}
