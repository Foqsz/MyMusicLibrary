using CommonTestUtilities.Entities;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens.Generator;
using Shouldly;
using System.Net;
using Xunit;

namespace WebApi.Test.Music.Register;
public class RegisterMusicTest : MyLibraryMusicBookClassFixture
{
    private readonly string method = "music";
    private readonly Guid _userIdentifier;

    public RegisterMusicTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _userIdentifier = factory.GetUserIdentifier();
    }

    [Fact]
    public async Task Success()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var request = RequestMusicJsonBuilder.Build();

        var musicRegister = await DoPost(method, request, token);

        musicRegister.StatusCode.ShouldBe(HttpStatusCode.Created);
    }
}
