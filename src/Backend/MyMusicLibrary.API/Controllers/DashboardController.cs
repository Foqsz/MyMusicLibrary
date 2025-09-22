using Microsoft.AspNetCore.Mvc;
using MyMusicLibrary.API.Attributes;
using MyMusicLibrary.Application.UseCases.Dashboard.GetUserMusicFavorites;
using MyMusicLibrary.Application.UseCases.Dashboard.RemoveMusicFavorite;
using MyMusicLibrary.Application.UseCases.DashBoard;
using MyMusicLibrary.Communication.Responses;

namespace MyMusicLibrary.API.Controllers;
[Route("[controller]")]
[ApiController]
[AuthenticatedUser]
public class DashboardController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(ResponseMusicsJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Get([FromServices] IDashboardUseCase useCase)
    {
        var result = await useCase.Execute();

        if(result.Musics.Any())
            return Ok(result);

        return NoContent();
    }

    [HttpGet("music-favorites")]
    [ProducesResponseType(typeof(ResponseMusicsJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetMusicFavorites([FromServices] IGetUserMusicFavoritesUseCase useCase)
    {
        var result = await useCase.Execute();

        return Ok(result);
    }

    [HttpDelete]
    [Route("{favoriteMusicId:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Unfavorite([FromServices] IUnfavoriteUseCase useCase, [FromRoute] long favoriteMusicId)
    {
        await useCase.Execute(favoriteMusicId);

        return NoContent();
    }
}
