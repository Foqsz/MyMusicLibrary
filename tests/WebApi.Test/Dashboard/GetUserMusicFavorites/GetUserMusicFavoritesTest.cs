using Azure;
using CommonTestUtilities.Tokens.Generator;
using Shouldly;
using System.Text.Json;
using Xunit;

namespace WebApi.Test.Dashboard.GetUserMusicFavorites;
public class GetUserMusicFavoritesTest : MyLibraryMusicBookClassFixture
{
    private const string method = "Dashboard/musicFavorites";
    private readonly Guid _userIdentifier;

    public GetUserMusicFavoritesTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _userIdentifier = factory.GetUserIdentifier();
    }

    [Fact]
    public async Task Success()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var result = await DoGet(method, token);

        result.IsSuccessStatusCode.ShouldBeTrue();
        result.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);

        await using var responseBody = await result.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("musics").GetArrayLength().ShouldBeGreaterThan(0);
    }
}
