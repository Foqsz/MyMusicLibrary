using Microsoft.AspNetCore.Mvc;
using MyMusicLibrary.API.Attributes;
using MyMusicLibrary.Application.UseCases.Playlist.Create;
using MyMusicLibrary.Application.UseCases.Playlist.Delete;
using MyMusicLibrary.Application.UseCases.Playlist.Update;
using MyMusicLibrary.Communication.Request;
using MyMusicLibrary.Communication.Responses;
using MyMusicLibrary.Exceptions.ExceptionsBase;

namespace MyMusicLibrary.API.Controllers;
[Route("[controller]")]
[ApiController]
[AuthenticatedUser]
public class PlaylistController : ControllerBase
{
    [HttpPost]
    [Route("create")]
    [ProducesResponseType(typeof(ResponsePlaylistJson), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreatePlaylist([FromServices] ICreatePlaylistUseCase useCase, [FromBody] RequestFromPlaylistJson request)
    {
        var result = await useCase.Execute(request);

        return Created(string.Empty, result);
    }

    [HttpDelete]
    [Route("delete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePlaylist([FromServices] IDeletePlaylistUseCase useCase, [FromQuery] long playlistId)
    {
        await useCase.Execute(playlistId);

        return NoContent();
    }

    [HttpPut]
    [Route("{id:long}")]
    [ProducesResponseType(typeof(ResponsePlaylistJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdatePlaylist([FromServices] IUpdatePlaylistUseCase useCase, [FromBody] RequestFromPlaylistJson request, long id)
    {
        try
        {
            var result = await useCase.Execute(id, request);
            return Ok(result);
        }
        catch (InvalidUpdateException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (PlaylistException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
