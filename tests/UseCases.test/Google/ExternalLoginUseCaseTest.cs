using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositores;
using CommonTestUtilities.Tokens.Generator;
using MyMusicLibrary.Application.UseCases.User.DoLogin.External;
using MyMusicLibrary.Domain.Extensions;
using Shouldly;
using Xunit;

namespace UseCases.Test.Google;
public class ExternalLoginUseCaseTest
{
    [Fact]
    public async Task Success_Register_Google()
    {
        (var user, var _) = UserBuilder.Build();

        var useCase = CreateUseCase(user, user.Name, user.Email, userNull: true);

        var request = useCase.Execute(user.Name, user.Email);

        await request.ShouldNotThrowAsync();
    }

    [Fact]
    public async Task Success_Login_Google()
    {
        (var user, var _) = UserBuilder.Build();

        var useCase = CreateUseCase(user, user.Name, user.Email, userNull: false);

        var request = useCase.Execute(user.Name, user.Email);

        await request.ShouldNotThrowAsync();
    }

    private static ExternalLoginUseCase CreateUseCase(MyMusicLibrary.Domain.Entities.User? user = null, string? name = null, string? email = null, bool userNull = true)
    {
        var userReadOnlyRepository = new UserReadOnlyRepositoryBuilder();
        var userWriteOnlyRepository = UserWriteOnlyRepositoryBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var accesTokenGenerator = JwtTokenGeneratorBuilder.Build();

        if (user is not null && userNull.IsFalse())
            userReadOnlyRepository.GetByEmail(user);

        return new ExternalLoginUseCase(userReadOnlyRepository.Build(), userWriteOnlyRepository, unitOfWork, accesTokenGenerator);
    }
}
