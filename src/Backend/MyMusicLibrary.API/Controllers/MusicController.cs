using Microsoft.AspNetCore.Mvc;
using MyMusicLibrary.API.Attributes;
using MyMusicLibrary.Application.UseCases.Music.Delete;
using MyMusicLibrary.Application.UseCases.Music.Favorite;
using MyMusicLibrary.Application.UseCases.Music.Genre;
using MyMusicLibrary.Application.UseCases.Music.GetById;
using MyMusicLibrary.Application.UseCases.Music.Register;
using MyMusicLibrary.Application.UseCases.Music.Search;
using MyMusicLibrary.Application.UseCases.Music.Upload;
using MyMusicLibrary.Communication.Request;
using MyMusicLibrary.Communication.Responses;
using MyMusicLibrary.Domain.Extensions;

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

        if (result.Any().IsFalse())
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseProfileMusicJson), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Register([FromServices] IRegisterMusicUseCase useCase, [FromBody] RequestMusicJson request)
    {
        var result = await useCase.Execute(request);
        return Created(string.Empty, result);
    }

    [HttpPost]
    [Route("favorite/{musicId:long}")]
    [ProducesResponseType(typeof(ResponseProfileMusicJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Favorite([FromServices] IFavoriteMusicUseCase useCase, [FromRoute] long musicId)
    {
        var result = await useCase.Execute(musicId);

        return Ok(result);
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

    [HttpPost("upload")] 
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Upload([FromServices] IUploadMusicUseCase useCase, [FromForm] RequestUploadMusicFormData file)
    {
        try
        {
            var result = await useCase.Execute(file);
            return Created(string.Empty, result );
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
