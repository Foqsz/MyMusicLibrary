using CommonTestUtilities.Tokens.Generator;
using Shouldly;
using System.Text.Json;
using Xunit;

namespace WebApi.Test.Dashboard;
public class GetDashboardTest : MyLibraryMusicBookClassFixture
{
    private const string METHOD = "Dashboard";

    private readonly Guid _userIdentifier;

    public GetDashboardTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _userIdentifier = webApplicationFactory.GetUserIdentifier();
    }

    [Fact]
    public async Task Success()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier); 

        var response = await DoGet(METHOD, token); 

        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("musics").GetArrayLength().ShouldBeGreaterThan(0);
    }
}
