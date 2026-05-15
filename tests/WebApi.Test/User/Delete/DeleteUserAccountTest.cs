using CommonTestUtilities.Tokens.Generator;
using Shouldly;
using System.Net;
using Xunit;

namespace WebApi.Test.User.Delete;
public class DeleteUserAccountTest : MyLibraryMusicBookClassFixture
{
    private readonly string method = "user";

    private readonly Guid _userIdentifier;

    public DeleteUserAccountTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _userIdentifier = factory.GetUserIdentifier();
    }

    [Fact]
    public async Task Success()
    {  
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var userDelete = await DoDelete(method, token);

        userDelete.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        userDelete.IsSuccessStatusCode.ShouldBeTrue();
    }
}
