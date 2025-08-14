using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens.Generator;
using Shouldly;
using System.Net;
using Xunit;

namespace WebApi.Test.User.Delete;
public class DeleteUserAccountInvalidTokenTest : MyLibraryMusicBookClassFixture
{
    private readonly string method = "user/update";

    private readonly Guid _userIdentifier;

    public DeleteUserAccountInvalidTokenTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _userIdentifier = factory.GetUserIdentifier();
    }

    [Fact]
    public async Task Error_Token_Invalid()
    {
        var user = RequestUpdateJsonBuilder.Build();

        var token = JwtTokenGeneratorBuilder.Build().Generate(Guid.NewGuid());

        var userUpdate = await DoPut(method: method, user, token);
        userUpdate.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Error_Token_Empty()
    {
        var user = RequestUpdateJsonBuilder.Build();

        var token = JwtTokenGeneratorBuilder.Build().Generate(Guid.NewGuid());

        var userUpdate = await DoPut(method: method, user, string.Empty);
        userUpdate.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Error_Token_Null()
    {
        var user = RequestUpdateJsonBuilder.Build();

        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var userUpdate = await DoPut(method: method, user, "");
        userUpdate.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    } 
}
