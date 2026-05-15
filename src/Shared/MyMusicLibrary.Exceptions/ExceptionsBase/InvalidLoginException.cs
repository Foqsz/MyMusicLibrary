using System.Net;

namespace MyMusicLibrary.Exceptions.ExceptionsBase;
public class InvalidLoginException : MyMusicLibraryException
{
    public InvalidLoginException() : base(ResourceMessagesException.LOGIN_INVALID)
    {
    }

    public override IList<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
}
