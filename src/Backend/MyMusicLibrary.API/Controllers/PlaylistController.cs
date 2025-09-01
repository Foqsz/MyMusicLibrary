using Microsoft.AspNetCore.Mvc;
using MyMusicLibrary.API.Attributes;
using MyMusicLibrary.Application.UseCases.Playlist.Create;
using MyMusicLibrary.Application.UseCases.Playlist.Delete;
using MyMusicLibrary.Communication.Request;
using MyMusicLibrary.Communication.Responses;

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
    public async Task<IActionResult> CreatePlaylist([FromServices] ICreatePlaylistUseCase useCase, [FromBody] RequestCreatePlaylistJson request)
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
}
