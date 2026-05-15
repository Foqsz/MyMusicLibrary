using Microsoft.AspNetCore.Mvc;
using MyMusicLibrary.API.Filters;

namespace MyMusicLibrary.API.Attributes;

public class AuthenticatedUserAttribute : TypeFilterAttribute
{
    public AuthenticatedUserAttribute() : base(typeof(AuthenticatedUserFilter))
    {
    }
}
