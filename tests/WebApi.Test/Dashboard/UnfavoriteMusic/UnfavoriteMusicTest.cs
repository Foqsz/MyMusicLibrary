using CommonTestUtilities.Tokens.Generator;
using MyMusicLibrary.Exceptions;
using Shouldly;
using Xunit;

namespace WebApi.Test.Dashboard.UnfavoriteMusic;
public class UnfavoriteMusicTest : MyLibraryMusicBookClassFixture
{
    private const string method = "dashboard";
    private readonly Guid _userIdentifier;
    private readonly long _musicIdFavorited;

    public UnfavoriteMusicTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _userIdentifier = factory.GetUserIdentifier();
        _musicIdFavorited = factory.GetUserMusicFavoriteMusicId();
    }

    [Fact]
    public async Task Success()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var result = await DoDelete($"{method}/{_musicIdFavorited}", token);

        result.StatusCode.ShouldBe(System.Net.HttpStatusCode.NoContent);
        result.ShouldNotBeNull();
    }

    [Fact]
    public async Task Error_No_Favorite_Music()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var result = await DoDelete($"{method}/{123}", token);

        result.StatusCode.ShouldBe(System.Net.HttpStatusCode.NotFound);
        
        var exception = await result.Content.ReadAsStringAsync();

        exception.ShouldContain(ResourceMessagesException.MUSIC_EMPTY);
    }
}
