using Microsoft.AspNetCore.Mvc;
using MyMusicLibrary.Application.UseCases.Token.RefreshToken;
using MyMusicLibrary.Communication.Request;
using MyMusicLibrary.Communication.Responses;

namespace MyMusicLibrary.API.Controllers;
[Route("[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    [HttpPost("refresh-token")]
    [ProducesResponseType(typeof(ResponseTokensJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> RefreshToken([FromServices] IUseRefreshTokenUseCase useCase, [FromBody] RequestNewTokenJson request)
    {
        var response = await useCase.Execute(request);

        return Ok(response);    
    }
}
