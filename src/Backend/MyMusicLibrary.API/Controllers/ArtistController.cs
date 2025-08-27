using Microsoft.AspNetCore.Mvc;
using MyMusicLibrary.API.Attributes;
using MyMusicLibrary.Application.UseCases.Artist;
using MyMusicLibrary.Communication.Responses;

namespace MyMusicLibrary.API.Controllers;
[Route("[controller]")]
[ApiController]
[AuthenticatedUser]
public class ArtistController : ControllerBase
{
    [HttpGet]
    [Route("search")]
    [ProducesResponseType(typeof(ResponseProfileArtistJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> SearchArtist([FromServices] ISearchArtistUseCase useCase, string name)
    {
        var result = await useCase.Execute(name); 

        if(result.Count == 0)
            return NotFound();

        return Ok(result);
    }
}
