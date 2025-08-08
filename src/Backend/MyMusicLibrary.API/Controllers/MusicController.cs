using Microsoft.AspNetCore.Mvc;
using MyMusicLibrary.API.Attributes;
using MyMusicLibrary.Application.UseCases.Music.Delete;
using MyMusicLibrary.Application.UseCases.Music.GetById;
using MyMusicLibrary.Application.UseCases.Music.Register;
using MyMusicLibrary.Communication.Request;
using MyMusicLibrary.Communication.Responses;

namespace MyMusicLibrary.API.Controllers;
[Route("api/[controller]")]
[ApiController]
[AuthenticatedUser]
public class MusicController : ControllerBase
{
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseRegisteredMusicJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetMusicById([FromServices] IGetMusicByIdUseCase useCase, int musicId)
    {
        var result = await useCase.Execute(musicId); 

        return Ok(result);
    }


    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredMusicJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> Register([FromServices] IRegisterMusicUseCase useCase, [FromBody] RequestMusicJson request)
    {
        var result = await useCase.Execute(request);
        return Created(string.Empty, result);
    }

    [HttpDelete] 
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Delete([FromServices] IDeleteMusicUseCase useCase, long musicId)
    {
        await useCase.Execute(musicId);
        return NoContent();
    }
}
