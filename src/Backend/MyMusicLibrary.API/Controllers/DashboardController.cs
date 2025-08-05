using Microsoft.AspNetCore.Mvc;
using MyMusicLibrary.API.Attributes;
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
}
