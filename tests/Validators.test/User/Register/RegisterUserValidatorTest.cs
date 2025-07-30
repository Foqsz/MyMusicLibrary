using MyMusicLibrary.Application.UseCases.User.Register;
using Xunit;
using CommonTestUtilities.Requests;
using Shouldly;
using MyMusicLibrary.Exceptions;

namespace Validators.Test.User.Register;
public class RegisterUserValidatorTest
{
    [Fact]
    public void Success()
    {
        var validate = new RegisterUserValidator();

        var request = RequestRegisterUserJsonBuilder.Build();

        var result = validate.Validate(request);

        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void Error_Email_Empty()
    {
        var validate = new RegisterUserValidator();

        var request = RequestRegisterUserJsonBuilder.Build();
        request.Email = string.Empty;   

        var result = validate.Validate(request);

        result.IsValid.ShouldBeFalse();
    }

    [Fact]
    public void Error_Email_Invalid()
    {
        var validate = new RegisterUserValidator();

        var request = RequestRegisterUserJsonBuilder.Build();
        request.Email = "oi.com";

        var result = validate.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.ErrorMessage == ResourceMessagesException.EMAIL_INVALID);
    }

    [Fact]
    public void Error_Name_Empty()
    {
        var validate = new RegisterUserValidator();

        var request = RequestRegisterUserJsonBuilder.Build();
        request.Name = string.Empty;

        var result = validate.Validate(request);

        result.IsValid.ShouldBeFalse();
    }

    [Fact]
    public void Error_Password_Empty()
    {
        var validate = new RegisterUserValidator();

        var request = RequestRegisterUserJsonBuilder.Build();
        request.Password = string.Empty;

        var result = validate.Validate(request);

        result.IsValid.ShouldBeFalse();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Error_Password_Invalid(int passwordLenght)
    {
        var validate = new RegisterUserValidator();

        var request = RequestRegisterUserJsonBuilder.Build(passwordLenght);

        var result = validate.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.ErrorMessage == ResourceMessagesException.PASSWORD_INVALID);
    }
}
