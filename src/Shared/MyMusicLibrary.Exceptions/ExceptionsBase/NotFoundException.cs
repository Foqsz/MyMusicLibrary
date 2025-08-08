using System.Net;

namespace MyMusicLibrary.Exceptions.ExceptionsBase;
public class NotFoundException : MyMusicLibraryException
{
    public NotFoundException(string message) : base(ResourceMessagesException.MUSIC_EMPTY)
    {
    }

    public override IList<string> GetErrorMessages() => [Message]; //mesma coisa que fazer new List<string> { Message };

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.NotFound;
}
