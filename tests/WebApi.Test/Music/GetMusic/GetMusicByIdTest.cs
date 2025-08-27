using CommonTestUtilities.Entities;
using CommonTestUtilities.Tokens.Generator;
using Shouldly;
using System.Net;
using Xunit;

namespace WebApi.Test.Music.GetMusic;
public class GetMusicByIdTest : MyLibraryMusicBookClassFixture
{
    private readonly string method = "music";
    private readonly Guid _userIdentifier;
    private readonly long _musicId;

    public GetMusicByIdTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _userIdentifier = factory.GetUserIdentifier();
        _musicId = factory.GetMusicId();
    }

    [Fact]
    public async Task Success()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var musicDelete = await DoGet($"{method}/{_musicId}", token);

        musicDelete.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Error_Music_Invalid_Id()
    {
        (var user, var _) = UserBuilder.Build();
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var music = MusicBuilder.Build(user);

        var musicDelete = await DoGet($"{method}/{123}", token);

        musicDelete.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }
}
