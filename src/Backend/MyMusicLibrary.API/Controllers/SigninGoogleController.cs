using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using MyMusicLibrary.Application.UseCases.User.DoLogin.External;
using MyMusicLibrary.Domain.Extensions;
using System.Security.Claims;

namespace MyMusicLibrary.API.Controllers;
[Route("[controller]")]
[ApiController]
public class SigninGoogleController : ControllerBase
{
    [HttpGet] 
    public async Task<IActionResult> LoginGoogle(string returnUrl, [FromServices] IExternalLoginUseCase useCase)
    {
        var authenticate = await Request.HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

        if (IsNotAuthenticated(authenticate))
            return Challenge(GoogleDefaults.AuthenticationScheme);
        else
        {
            var claims = authenticate.Principal!.Identities.First().Claims;

            var name = claims.First(c => c.Type == ClaimTypes.Name).Value;
            var email = claims.First(c => c.Type == ClaimTypes.Email).Value;

            var token = await useCase.Execute(name, email);

            return Redirect($"{returnUrl}/{token}");
        }
    }
    protected static bool IsNotAuthenticated(AuthenticateResult authenticate)
    {
        return authenticate.Succeeded.IsFalse()
            || authenticate.Principal is null
            || authenticate.Principal.Identities.Any(id => id.IsAuthenticated).IsFalse();
    }
}
