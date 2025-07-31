using Microsoft.AspNetCore.Mvc;
using MyMusicLibrary.Application.UseCases.User.DoLogin;
using MyMusicLibrary.Communication.Request;
using MyMusicLibrary.Communication.Responses;

namespace MyMusicLibrary.API.Controllers;
[Route("[controller]")]
[ApiController]
public class LoginController : ControllerBase 
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromServices] IDoLoginUseCase useCase, [FromBody] RequestDoLoginJson request)
    {
        var response = await useCase.Execute(request);

        return Ok(response);
    }
}
