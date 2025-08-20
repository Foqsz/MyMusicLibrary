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

    public DeleteMusicTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _userIdentifier = factory.GetUserIdentifier();
    }

    [Fact]
    public async Task Success()
    {
        (var user, var _) = UserBuilder.Build();
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var music = MusicBuilder.Build(user);

        var musicDelete = await DoDelete($"{method}/{music.Id}", token);

        musicDelete.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Error_Music_Invalid_Id()
    {
        (var user, var _) = UserBuilder.Build();
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier); 

        var musicDelete = await DoDelete($"{method}/{2}", token);

        musicDelete.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }
}
