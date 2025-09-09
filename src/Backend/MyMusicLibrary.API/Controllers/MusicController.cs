using Microsoft.AspNetCore.Mvc;
using MyMusicLibrary.API.Attributes;
using MyMusicLibrary.Application.UseCases.Music.Delete;
using MyMusicLibrary.Application.UseCases.Music.Genre;
using MyMusicLibrary.Application.UseCases.Music.GetById;
using MyMusicLibrary.Application.UseCases.Music.Register;
using MyMusicLibrary.Application.UseCases.Music.Search;
using MyMusicLibrary.Communication.Request;
using MyMusicLibrary.Communication.Responses;

namespace MyMusicLibrary.API.Controllers;
[Route("[controller]")]
[ApiController]
[AuthenticatedUser]
public class MusicController : ControllerBase
{
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseProfileMusicJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetMusicById([FromServices] IGetMusicByIdUseCase useCase, long id)
    {
        var result = await useCase.Execute(id); 

        return Ok(result);
    }

    [HttpGet]
    [Route("search")]
    [ProducesResponseType(typeof(ResponseProfileMusicJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> SearchMusic([FromServices] ISearchMusicUseCase useCase, string name)
    {
        var result = await useCase.Execute(name);

        return Ok(result);
    }

    [HttpGet]
    [Route("genre")]
    [ProducesResponseType(typeof(ResponseGenreJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetGenre([FromServices] IGetGenreUseCase useCase)
    {
        var result = await useCase.Execute();

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseProfileMusicJson), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromServices] IRegisterMusicUseCase useCase, [FromBody] RequestMusicJson request)
    {
        var result = await useCase.Execute(request);
        return Created(string.Empty, result);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromServices] IDeleteMusicUseCase useCase, [FromRoute] long id)
    {
        await useCase.Execute(id);
        return NoContent();
    }
}
