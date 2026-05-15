using CommonTestUtilities.Entities;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens.Generator;
using Shouldly;
using System.Net;
using Xunit;

namespace WebApi.Test.User.Update;
public class UpdateUserAccountTest : MyLibraryMusicBookClassFixture
{
    private readonly string method = "user/update";

    private readonly Guid _userIdentifier;
    private readonly string _userEmail;

    public UpdateUserAccountTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _userIdentifier = factory.GetUserIdentifier();
        _userEmail = factory.GetEmail();
    }

    [Fact]
    public async Task Success()
    {
        var user = RequestUpdateJsonBuilder.Build();

        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var userUpdate = await DoPut(method: method, user, token);
        userUpdate.StatusCode.ShouldBe(HttpStatusCode.OK); 
    }

    [Fact]
    public async Task Error_Name_Empty()
    {
        var user = RequestUpdateJsonBuilder.Build();
        user.Name = string.Empty;

        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var userUpdate = await DoPut(method: method, user, token);
        userUpdate.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Error_Email_Empty()
    {
        var user = RequestUpdateJsonBuilder.Build();
        user.Email = string.Empty;

        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var userUpdate = await DoPut(method: method, user, token);
        userUpdate.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Error_Email_Invalid()
    {
        var user = RequestUpdateJsonBuilder.Build();
        user.Email = "monza.com";

        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var userUpdate = await DoPut(method: method, user, token);
        userUpdate.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Error_Email_Exists()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        var response = await DoPost(method: "user", request: request);

        response.StatusCode.ShouldBe<HttpStatusCode>(HttpStatusCode.Created);

        var user = RequestUpdateJsonBuilder.Build();
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);
        user.Email = request.Email;

        var userUpdate = await DoPut(method: method, user, token);
        userUpdate.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }
}
