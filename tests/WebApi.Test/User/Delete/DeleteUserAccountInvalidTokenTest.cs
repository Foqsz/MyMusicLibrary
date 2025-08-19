using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens.Generator;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WebApi.Test.User.Delete;
public class DeleteUserAccountInvalidTokenTest : MyLibraryMusicBookClassFixture
{
    private readonly string method = "user";
    private readonly Guid _userIdentifier;

    public DeleteUserAccountInvalidTokenTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _userIdentifier = factory.GetUserIdentifier();
    }

    [Fact]
    public async Task Error_Token_Invalid()
    {  
        var token = JwtTokenGeneratorBuilder.Build().Generate(Guid.NewGuid());

        var userUpdate = await DoDelete(method: method, token);
        userUpdate.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Error_Token_Null()
    {
        var user = RequestUpdateJsonBuilder.Build();

        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        var userUpdate = await DoDelete(method: method, "");
        userUpdate.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }
}
