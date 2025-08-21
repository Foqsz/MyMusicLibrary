using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens.Generator;
using Shouldly;
using System.Net;
using Xunit;

namespace WebApi.Test.User.ChangePassword;
public class UserChangePasswordInvalidTokenTest : MyLibraryMusicBookClassFixture
{
    private const string METHOD = "user/change-password";
    private readonly Guid _userIdentifier;
    private readonly string _password;
    public UserChangePasswordInvalidTokenTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _userIdentifier = factory.GetUserIdentifier();
        _password = factory.GetPassword();
    }

    [Fact] 
    public async Task Error_Token_Invalid()
    {
        var userChangePassword = RequestUserChangePasswordBuilder.Build();
        userChangePassword.CurrentPassword = _password;

        var token = JwtTokenGeneratorBuilder.Build().Generate(Guid.NewGuid());

        var userUpdate = await DoPut(method: METHOD, userChangePassword, token);
        userUpdate.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Error_Token_Null()
    {
        var userChangePassword = RequestUserChangePasswordBuilder.Build();

        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var userUpdate = await DoPut(method: METHOD, userChangePassword, token: "");
        userUpdate.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }
}
