using System.Net;

namespace MyMusicLibrary.Exceptions.ExceptionsBase;
public class InvalidUpdateException : MyMusicLibraryException
{
    public InvalidUpdateException(string message) : base(message)
    {
    }

    public override IList<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
}
