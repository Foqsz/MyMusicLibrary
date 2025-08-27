using CommonTestUtilities.Entities;
using CommonTestUtilities.Tokens.Generator;
using Shouldly;
using System.Net;
using Xunit;

namespace WebApi.Test.Music.Delete;
public class DeleteMusicTest : MyLibraryMusicBookClassFixture
{
    private readonly string method = "music";
    private readonly Guid _userIdentifier;
    private readonly long _musicId;

    public DeleteMusicTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _userIdentifier = factory.GetUserIdentifier();
        _musicId = factory.GetMusicId();
    }

    [Fact]
    public async Task Success()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var musicDelete = await DoDelete($"{method}/{_musicId}", token);

        musicDelete.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Error_Music_Invalid_Id()
    { 
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier); 

        var musicDelete = await DoDelete($"{method}/{1000}", token);

        musicDelete.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }
}
