using CommonTestUtilities.Requests;
using MyMusicLibrary.Exceptions;
using Shouldly;
using System.Globalization;
using System.Net;
using System.Text.Json;
using WebApi.Test.InlineData;
using Xunit;

namespace WebApi.Test.User.Register;
public class RegisterUserTest : MyLibraryMusicBookClassFixture
{
    private readonly string method = "user";

    public RegisterUserTest(CustomWebApplicationFactory factory) : base(factory) { }

    [Fact]
    public async Task Sucess()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        var response = await DoPost(method: method, request: request);

        response.StatusCode.ShouldBe<HttpStatusCode>(HttpStatusCode.Created);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        //acessa o documento, esse documento tem uma propriedade "name", pego como string o valor dessa propriedade
        responseData.RootElement.GetProperty("name").GetString().ShouldNotBeNullOrWhiteSpace(request.Name);
        //responseData.RootElement.GetProperty("tokens").GetProperty("accessToken").GetString().ShouldNotBeNullOrEmpty();
    }


    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Empty_Name(string culture)
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Name = string.Empty;

        var response = await DoPost(method: method, request: request, culture: culture);

        response.StatusCode.ShouldBe<HttpStatusCode>(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        var expectedMessage = ResourceMessagesException.ResourceManager.GetString("NAME_EMPTY", new CultureInfo(culture));

        errors.ShouldContain(error => error.GetString()!.Equals(expectedMessage));
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Invalid_Password(string culture)
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Password = string.Empty;

        var response = await DoPost(method: method, request: request, culture: culture);

        response.StatusCode.ShouldBe<HttpStatusCode>(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        var expectedMessage = ResourceMessagesException.ResourceManager.GetString("PASSWORD_EMPTY", new CultureInfo(culture));

        errors.ShouldContain(error => error.GetString()!.Equals(expectedMessage));
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Email_Already_Registered(string culture)
    {
        // Arrange: Register a user with a specific email
        var request = RequestRegisterUserJsonBuilder.Build();

        // First registration should succeed
        var firstResponse = await DoPost(method: method, request: request, culture: culture);
        firstResponse.StatusCode.ShouldBe(HttpStatusCode.Created);

        // Act: Try to register again with the same email
        var duplicateRequest = RequestRegisterUserJsonBuilder.Build();
        duplicateRequest.Email = request.Email;

        var response = await DoPost(method: method, request: duplicateRequest, culture: culture);

        // Assert: Should return BadRequest with the correct error message
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();
        var responseData = await JsonDocument.ParseAsync(responseBody);
        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        var expectedMessage = ResourceMessagesException.ResourceManager.GetString("EMAIL_ALREADY_REGISTERED", new CultureInfo(culture));
        errors.ShouldContain(error => error.GetString()!.Equals(expectedMessage));
    }
}

