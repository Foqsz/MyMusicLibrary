using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens.Generator;
using Shouldly;
using System.Net;
using Xunit;

namespace WebApi.Test.User.ChangePassword;
public class UserChangePasswordTest : MyLibraryMusicBookClassFixture
{
    private const string METHOD = "user/change-password";
    private readonly Guid _userIdentifier;
    private readonly string _password;

    public UserChangePasswordTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _userIdentifier = factory.GetUserIdentifier();
        _password = factory.GetPassword();
    }

    [Fact]
    public async Task Success()
    {
        var userChangePassword = RequestUserChangePasswordBuilder.Build();
        userChangePassword.CurrentPassword = _password;

        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var userUpdate = await DoPut(method: METHOD, userChangePassword, token);
        userUpdate.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Error_Invalid_Current_Password()
    {
        var userChangePassword = RequestUserChangePasswordBuilder.Build(); 

        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var userUpdate = await DoPut(method: METHOD, userChangePassword, token);
        userUpdate.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Error_Same_Password()
    {
        var userChangePassword = RequestUserChangePasswordBuilder.Build();
        userChangePassword.CurrentPassword = _password;
        userChangePassword.NewPassword = _password;

        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var userUpdate = await DoPut(method: METHOD, userChangePassword, token);
        userUpdate.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Theory]
    [InlineData("1")]
    [InlineData("2")]
    [InlineData("3")]
    [InlineData("4")]
    [InlineData("5")]
    public async Task Error_Invalid_Lenght_Password(string passwordLenght)
    {
        var userChangePassword = RequestUserChangePasswordBuilder.Build();
        userChangePassword.CurrentPassword = _password;
        userChangePassword.NewPassword = passwordLenght;

        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var userUpdate = await DoPut(method: METHOD, userChangePassword, token);
        userUpdate.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Error_Empty_Password()
    {
        var userChangePassword = RequestUserChangePasswordBuilder.Build();
        userChangePassword.CurrentPassword = _password;
        userChangePassword.NewPassword = "";

        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var userUpdate = await DoPut(method: METHOD, userChangePassword, token);
        userUpdate.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }
}
