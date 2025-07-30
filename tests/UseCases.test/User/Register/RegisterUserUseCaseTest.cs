using CommonTestUtilities.Cryptografhy;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositores;
using CommonTestUtilities.Requests;
using MyMusicLibrary.Application.UseCases.User.Register;
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

        var useCase = CreateUseCase();

        var result = await useCase.Execute(request);

        result.ShouldNotBeNull(); 
        result.Name.ShouldBe(request.Name); 
    }

    [Fact]
    public async Task Error_User_Name_Empty()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        request.Name = string.Empty;

        var useCase = CreateUseCase(); 

        Func<Task> act = async () => await useCase.Execute(request);

        var exception = await act.ShouldThrowAsync<ErrorOnValidationException>();

        exception.GetErrorMessages().Count.ShouldBe(1);

        exception.GetErrorMessages().First().ShouldBe(ResourceMessagesException.NAME_EMPTY);
    }

    [Fact]
    public async Task Error_User_Email_Exists_Active()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        var useCase = CreateUseCase(request.Email);

        Func<Task> act = async () => await useCase.Execute(request); 

        var exception = await act.ShouldThrowAsync<ErrorOnValidationException>();
        exception.GetErrorMessages().Count.ShouldBe(1);
        exception.GetErrorMessages().First().ShouldBe(ResourceMessagesException.EMAIL_ALREADY_REGISTERED);
    }

    private static RegisterUserUseCase CreateUseCase(string? email = null)
    {
        var repositoryWriteOnly = UserWriteOnlyRepositoryBuilder.Build();
        var repositoryReadOnly = new UserReadOnlyRepositoryBuilder();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var passwordEncripter = PasswordEncripterBuilder.Build();
        var mapper = MapperBuilder.Build();

        if (email is not null)
            repositoryReadOnly.ExistActiveUserWithEmail(email);

        return new RegisterUserUseCase(
            repositoryWriteOnly,
            repositoryReadOnly.Build(),
            unitOfWork,
            mapper,
            passwordEncripter
        );
    }
}
