using Microsoft.AspNetCore.Mvc;
using MyMusicLibrary.API.Attributes;
using MyMusicLibrary.Application.UseCases.Playlist.AddMusicToPlaylist;
using MyMusicLibrary.Application.UseCases.Playlist.Create;
using MyMusicLibrary.Application.UseCases.Playlist.Delete;
using MyMusicLibrary.Application.UseCases.Playlist.GetPlaylistAll;
using MyMusicLibrary.Application.UseCases.Playlist.GetPlaylistId;
using MyMusicLibrary.Application.UseCases.Playlist.GetPlaylistName;
using MyMusicLibrary.Application.UseCases.Playlist.Update;
using MyMusicLibrary.Communication.Request;
using MyMusicLibrary.Communication.Responses;
using MyMusicLibrary.Domain.Extensions;
using MyMusicLibrary.Exceptions.ExceptionsBase;

namespace MyMusicLibrary.API.Controllers;
[Route("[controller]")]
[ApiController]
[AuthenticatedUser]
public class PlaylistController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(ResponsePlaylistAllJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetPlaylistAll([FromServices] IGetPlaylistAllUseCase useCase)
    {
        var result = await useCase.Execute();

        if (result.Playlists.Any())
            return Ok(result);

        return NotFound();
    }

    [HttpGet]
    [Route("search")]
    [ProducesResponseType(typeof(ResponsePlaylistAllJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetPlaylistName([FromServices] IGetPlaylistNameUseCase useCase, [FromQuery] string name)
    {
        try
        {
            var result = await useCase.Execute(name);

            return Ok(result);
        }
        catch (PlaylistException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet]
    [Route("{id:long}")]
    [ProducesResponseType(typeof(ResponsePlaylistAllJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetPlaylist([FromServices] IGetPlaylistIdUseCase useCase, long id)
    {
        var result = await useCase.Execute(id);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    [Route("create")]
    [ProducesResponseType(typeof(ResponsePlaylistJson), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreatePlaylist([FromServices] ICreatePlaylistUseCase useCase, [FromBody] RequestFromPlaylistJson request)
    {
        var result = await useCase.Execute(request);

        return Created(string.Empty, result);
    }

    [HttpDelete]
    [Route("delete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

    [HttpPost]
    [Route("add-music")]
    [ProducesResponseType(typeof(ResponseMusicPlaylistJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> AddMusicToPlaylist([FromServices] IAddMusicToPlaylistUseCase useCase, RequestMusicPlaylistJson request)
    {
        var result = await useCase.Execute(request);

        return Ok(result);
    }
}
