using CommonTestUtilities.Tokens.Generator;
using Shouldly;
using Xunit;

namespace WebApi.Test.User.Data;
public class GetUserDataTest : MyLibraryMusicBookClassFixture
{
    private const string method = "user/data";
    private readonly Guid _userIdentifier; 

    public GetUserDataTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _userIdentifier = factory.GetUserIdentifier();
    }

    [Fact]
    public async Task Success()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var response = await DoGet(method, token);

        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);
    }
}
