using CommonTestUtilities.Entities;
using CommonTestUtilities.Tokens.Generator;
using Shouldly;
using Xunit;

namespace WebApi.Test.Artist;
public class SearchArtistTest : MyLibraryMusicBookClassFixture
{
    private readonly string method = "artist/search";
    private readonly Guid _userIdentifier;
    private readonly string _artist;

    public SearchArtistTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _userIdentifier = factory.GetUserIdentifier();
        _artist = factory.GetArtistName();
    }

    [Fact]
    public async Task Success()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier); 

        var response = await DoGet($"{method}?name={_artist}", token);

        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);
    }

    [Fact]
    public async Task Error_Artist_NotFound()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var response = await DoGet($"{method}?name={25}", token);

        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.NotFound);
    }
}
