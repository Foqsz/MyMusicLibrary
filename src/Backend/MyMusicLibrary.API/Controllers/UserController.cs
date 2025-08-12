using Microsoft.AspNetCore.Mvc;
using MyMusicLibrary.API.Attributes;
using MyMusicLibrary.Application.UseCases.User.Data;
using MyMusicLibrary.Application.UseCases.User.Delete;
using MyMusicLibrary.Application.UseCases.User.Register;
using MyMusicLibrary.Communication.Request;
using MyMusicLibrary.Communication.Responses;

namespace MyMusicLibrary.API.Controllers;
[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [AuthenticatedUser]
    [HttpGet("data")]
    [ProducesResponseType(typeof(ResponseDataUser), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetData([FromServices] IGetUserDataUseCase useCase)
    {
        var result = await useCase.Execute();
        return Ok(result);
    }


    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> Register([FromServices] IRegisterUserUseCase useCase, [FromBody] RequestRegisterUserJson request)
    {
        var result = await useCase.Execute(request);

        return Created(string.Empty, result);
    }

    [AuthenticatedUser]
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete([FromServices] IDeleteUserUseCase useCase)
    {
        await useCase.Execute();

        return NoContent();
    }
}
