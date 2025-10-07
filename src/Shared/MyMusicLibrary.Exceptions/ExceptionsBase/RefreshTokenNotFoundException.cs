using System.Net;

namespace MyMusicLibrary.Exceptions.ExceptionsBase;
public class RefreshTokenNotFoundException : MyMusicLibraryException
{
    public RefreshTokenNotFoundException() : base(ResourceMessagesException.EXPIRED_SESSION)
    {
    }

    public override IList<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
}
