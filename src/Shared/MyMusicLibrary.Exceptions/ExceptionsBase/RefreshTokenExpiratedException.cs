using System.Net;

namespace MyMusicLibrary.Exceptions.ExceptionsBase;
public class RefreshTokenExpiredException : MyMusicLibraryException
{
    public RefreshTokenExpiredException() : base(ResourceMessagesException.INVALID_SESSION)
    {
    }

    public override IList<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Forbidden;
}