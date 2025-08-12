using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositores;
using MyMusicLibrary.Application.UseCases.User.Delete;
using Shouldly;
using Xunit;

namespace UseCases.Test.User.Delete;
public class DeleteUserAccountUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var user, var _) = UserBuilder.Build();
        var music = MusicBuilder.Build(user);

        var useCase = CreateUseCase(user);

        var result = useCase.Execute();

        await result.ShouldNotThrowAsync();
    }

    private static DeleteUserAccountUseCase CreateUseCase(MyMusicLibrary.Domain.Entities.User? user)
    {
        var loggedUser = LoggedUserBuilder.Build(user!);
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
            userReadOnlyRepository.Build(),
            unitOfWork
        );
    }
}
