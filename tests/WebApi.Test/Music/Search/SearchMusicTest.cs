using CommonTestUtilities.Entities;
using CommonTestUtilities.Tokens.Generator;
using Shouldly;
using System.Text.Json;
using Xunit;

namespace WebApi.Test.Music.Search;
public class SearchMusicTest : MyLibraryMusicBookClassFixture
{
    private readonly string method = "music/search";
    private readonly Guid _userIdentifier;
    private readonly string _musicName;

    public SearchMusicTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _userIdentifier = factory.GetUserIdentifier();
        _musicName = factory.GetMusicName();
    }

    [Fact]
    public async Task Success()
    { 
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier); 

        var response = await DoGet($"{method}?name={_musicName}", token);

        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("musics").GetArrayLength().ShouldBeGreaterThan(0);
    }

    [Fact]
    public async Task Error_Music_Not_Exist()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var response = await DoGet($"{method}?name=alpha", token);

        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.NotFound); 
    }
}
