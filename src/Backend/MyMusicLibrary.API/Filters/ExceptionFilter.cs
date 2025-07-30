
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
        if (context.Exception is MyMusicLibraryException)
            HandleProjectException(context);
        else
            ThrowUnknowException(context);
    }

    private void HandleProjectException(ExceptionContext context)
    {
        if (context.Exception is ErrorOnValidationException)
        {
            var exception = context.Exception as ErrorOnValidationException;

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new BadRequestObjectResult(new ResponseErrorJson(exception!.GetErrorMessages()));
        }
    }

    private void ThrowUnknowException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new ResponseErrorJson(ResourceMessagesException.UNKNOWN_ERROR));
    }
}
