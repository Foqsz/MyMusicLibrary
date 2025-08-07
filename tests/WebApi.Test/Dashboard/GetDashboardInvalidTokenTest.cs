using CommonTestUtilities.Entities;
using CommonTestUtilities.Tokens.Generator;
using Shouldly;
using System.Net;
using Xunit;

namespace WebApi.Test.Dashboard;
public class GetDashboardInvalidTokenTest : MyLibraryMusicBookClassFixture
{
    private const string METHOD = "dashboard";

    public GetDashboardInvalidTokenTest(CustomWebApplicationFactory webApplication) : base(webApplication)
    {
    }

    [Fact]
    public async Task Error_Token_Invalid()
    {
        var response = await DoGet(METHOD, token: "tokenInvalid");

        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Error_Token_Empty()
    {
        var response = await DoGet(METHOD, token: "");
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Error_Token_Null()
    {
        var response = await DoGet(METHOD, token: null!);
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Error_Token_User_Not_Found()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(Guid.NewGuid());
          
        var response = await DoGet(METHOD, token: token);
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }
}
