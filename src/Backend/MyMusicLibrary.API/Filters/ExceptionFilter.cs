using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using MyMusicLibrary.Communication.Responses;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;

namespace MyMusicLibrary.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ExceptionFilter> _logger;

    public ExceptionFilter(ILogger<ExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        if (context.Exception is MyMusicLibraryException myLibraryMusicException)
        {
            HandleProjectException(myLibraryMusicException, context);
        }
        else
        {
            ThrowUnknownException(context);
        }
    }

    private void HandleProjectException(MyMusicLibraryException ex, ExceptionContext context)
    {
        // Loga o erro customizado
        _logger.LogWarning(ex, "Erro de regra de negócio capturado");

        context.HttpContext.Response.StatusCode = (int)ex.GetStatusCode();
        context.Result = new ObjectResult(new ResponseErrorJson(ex.GetErrorMessages()));
    }

    private void ThrowUnknownException(ExceptionContext context)
    {
        // Loga o stacktrace completo
        _logger.LogError(context.Exception, "Erro inesperado ocorreu");

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(new ResponseErrorJson(ResourceMessagesException.UNKNOWN_ERROR));
    }
}