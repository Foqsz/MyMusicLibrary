using CommonTestUtilities.Tokens.Generator;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace WebApi.Test.Music.Favorite;
public class FavoriteMusicTest : MyLibraryMusicBookClassFixture
{
    private const string method = "music/favorite";
    private readonly Guid _userIdentifier;
    private readonly long _musicId;

    public FavoriteMusicTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _userIdentifier = factory.GetUserIdentifier();
        _musicId = factory.GetMusicId();
    }

    [Fact]
    public async Task Success()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var request = _musicId;

        var result = await DoPost($"{method}/{_musicId}", request, token);

        result.IsSuccessStatusCode.ShouldBeTrue();
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK); 
    }

    [Fact]
    public async Task Error_Music_NotFound()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var request = _musicId;

        var result = await DoPost($"{method}/{123}", request, token);

        result.IsSuccessStatusCode.ShouldBeFalse();
        result.StatusCode.ShouldBe(System.Net.HttpStatusCode.BadRequest);
    }
}
