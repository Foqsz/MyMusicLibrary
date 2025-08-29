using System.Net;

namespace MyMusicLibrary.Exceptions.ExceptionsBase;
public class PlaylistException : MyMusicLibraryException
{
    public PlaylistException(string message) : base(message)
    {
    }
    public override IList<string> GetErrorMessages() => [Message];
    public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
}
