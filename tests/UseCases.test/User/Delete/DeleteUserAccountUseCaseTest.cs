using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositores;
using MyMusicLibrary.Application.UseCases.User.Delete;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;
using Shouldly;
using Xunit;

namespace UseCases.Test.User.Delete;
public class DeleteUserAccountUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var user, var _) = UserBuilder.Build();

        var useCase = CreateUseCase(user);

        var result = useCase.Execute();

        await result.ShouldNotThrowAsync();
    }

    [Fact]
    public async Task Error_User_Inactive()
    {
        (var user, var _) = UserBuilder.Build();
        user.Active = false;

        var useCase = CreateUseCase(user);

        var exception = await Should.ThrowAsync<InvalidActionException>(() => useCase.Execute());

        exception.Message.ShouldBe(ResourceMessagesException.ERROR_USER_IS_INACTIVE);
    }

    private static DeleteUserAccountUseCase CreateUseCase(MyMusicLibrary.Domain.Entities.User user)
    {
        var loggedUser = LoggedUserBuilder.Build(user);
        var userDeleteAccountRepository = UserDeleteAccountRepositoryBuilder.Build();
        var userReadOnlyRepository = new UserReadOnlyRepositoryBuilder();
        var unitOfWork = UnitOfWorkBuilder.Build();

        if (user is not null)
        {
            userReadOnlyRepository.ExistActiveUserWithIdentifier(user.UserIdentifier);
            userReadOnlyRepository.GetById(user);
        } 

        return new DeleteUserAccountUseCase( 
            loggedUser,
            userDeleteAccountRepository,
            unitOfWork
        );
    }
}
