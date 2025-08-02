using CommonTestUtilities.Requests;
using MyMusicLibrary.Exceptions;
using Shouldly;
using System.Net;
using System.Text.Json;
using Xunit;

namespace WebApi.Test.User.DoLogin;
public class DoLoginUserTest : MyLibraryMusicBookClassFixture
{
    private readonly string method = "login";

    public DoLoginUserTest(CustomWebApplicationFactory factory) : base(factory) { }

    [Fact]
    public async Task Sucess()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        var firstResponse = await DoPost(method: "user", request: request);
        firstResponse.StatusCode.ShouldBe(HttpStatusCode.Created);

        var response = await DoPost(method: method, request: request);

        response.StatusCode.ShouldBe<HttpStatusCode>(HttpStatusCode.OK);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);
         
        responseData.RootElement.GetProperty("tokens").GetProperty("accessToken").GetString().ShouldNotBeNullOrEmpty();
    }

    [Fact]
    public async Task Error_Email_Invalid()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        var firstResponse = await DoPost(method: "user", request: request);
        firstResponse.StatusCode.ShouldBe(HttpStatusCode.Created);

        request.Email = "invalid-email";
        var response = await DoPost(method: method, request: request);

        response.StatusCode.ShouldBe<HttpStatusCode>(HttpStatusCode.Unauthorized);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        errors.ShouldContain(error => error.GetString()!.Equals(ResourceMessagesException.LOGIN_INVALID));
    }

    [Fact]
    public async Task Error_Password_Invalid()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        var firstResponse = await DoPost(method: "user", request: request);
        firstResponse.StatusCode.ShouldBe(HttpStatusCode.Created);

        request.Password = "123456789";
        var response = await DoPost(method: method, request: request);

        response.StatusCode.ShouldBe<HttpStatusCode>(HttpStatusCode.Unauthorized);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        errors.ShouldContain(error => error.GetString()!.Equals(ResourceMessagesException.LOGIN_INVALID));
    }
}
