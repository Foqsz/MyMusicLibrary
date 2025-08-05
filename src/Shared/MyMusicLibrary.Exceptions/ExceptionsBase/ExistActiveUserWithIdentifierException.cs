using System.Net;

namespace MyMusicLibrary.Exceptions.ExceptionsBase;
public class ExistActiveUserWithIdentifierException : MyMusicLibraryException
{
    public ExistActiveUserWithIdentifierException(string message) : base(message)
    {
    }
    public override IList<string> GetErrorMessages() => [Message];
    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
}
