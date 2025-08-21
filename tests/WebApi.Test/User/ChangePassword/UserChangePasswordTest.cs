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
}
