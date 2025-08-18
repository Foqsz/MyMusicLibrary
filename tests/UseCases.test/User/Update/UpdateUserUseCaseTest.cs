using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositores;
using CommonTestUtilities.Requests;
using MyMusicLibrary.Application.UseCases.User.Update;
using MyMusicLibrary.Communication.Request;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;
using Shouldly;
using Xunit;

namespace UseCases.Test.User.Update;
public class UpdateUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var user, var _) = UserBuilder.Build(); 
        var request = RequestUpdateJsonBuilder.Build();
         
        request.Email = user.Email;

        var useCase = CreateUseCase(user, request);

        var result = useCase.Execute(request);

        await result.ShouldNotThrowAsync(); 
    }

    [Fact]
    public async Task Error_User_Email()
    {
        (var user, var _) = UserBuilder.Build();
        var request = RequestUpdateJsonBuilder.Build(); 

        var useCase = CreateUseCase(user, request);

        Func<Task> act = async () => await useCase.Execute(request);

        var exception = await act.ShouldThrowAsync<ErrorOnValidationException>();

        exception.GetErrorMessages().Count.ShouldBe(1);

        exception.GetErrorMessages().First().ShouldBe(ResourceMessagesException.EMAIL_ALREADY_REGISTERED);
    }

    private static UpdateUserUseCase CreateUseCase(MyMusicLibrary.Domain.Entities.User user,RequestUpdateUserJson request)
    {
        var unitOfWork = UnitOfWorkBuilder.Build();
        var repositoryWriteOnly = UserUpdateWriteOnlyRepositoryBuilder.Build();
        var repositoryReadOnly = new UserReadOnlyRepositoryBuilder();
        var loggedUser = LoggedUserBuilder.Build(user);

        if(request.Email is not null)
            repositoryReadOnly.ExistActiveUserWithEmail(request.Email);

        if (user is not null)
            repositoryReadOnly.GetById(user);

        return new UpdateUserUseCase(unitOfWork, repositoryWriteOnly, repositoryReadOnly.Build(), loggedUser);
    }
}
