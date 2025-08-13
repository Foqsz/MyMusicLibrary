using System.Net;

namespace MyMusicLibrary.Exceptions.ExceptionsBase;
public class InvalidUpdateException : MyMusicLibraryException
{
    public InvalidUpdateException(string message) : base(ResourceMessagesException.UPDATE_INVALID)
    {
    }

    public override IList<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
}
