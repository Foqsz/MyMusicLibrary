using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using MyMusicLibrary.Application.UseCases.User.Data;
using Shouldly;
using Xunit;

namespace UseCases.Test.User.Data;
public class GetDataUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var user = UserBuilder.Build().user;

        var useCase = CreateUseCase(user);

        var result = await useCase.Execute();

        result.ShouldNotBeNull();
        result.Name.ShouldBe(user.Name);
        result.Email.ShouldBe(user.Email); 
        result.CreatedOn.ShouldBe(user.CreatedOn);
    } 

    private static GetUserDataUseCase CreateUseCase(MyMusicLibrary.Domain.Entities.User user)
    {
        var loggedUser = LoggedUserBuilder.Build(user);
        var mapper = MapperBuilder.Build();

        return new GetUserDataUseCase(loggedUser, mapper);
    }
}
