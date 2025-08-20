
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyMusicLibrary.Communication.Responses;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;
using System.Net;

namespace MyMusicLibrary.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is MyMusicLibraryException myLibraryMusicException)
            HandleProjectException(myLibraryMusicException, context);
        else
            ThrowUnknowException(context);
    }

    private static void HandleProjectException(MyMusicLibraryException myLibraryMusicException, ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)myLibraryMusicException.GetStatusCode();
        context.Result = new ObjectResult(new ResponseErrorJson(myLibraryMusicException.GetErrorMessages()));
    }

    private static void ThrowUnknowException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(new ResponseErrorJson(ResourceMessagesException.UNKNOWN_ERROR));
    }
}
