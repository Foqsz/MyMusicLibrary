using System.Net;

namespace MyMusicLibrary.Exceptions.ExceptionsBase;
public class UserChangePasswordException : MyMusicLibraryException
{
    public UserChangePasswordException(string message) : base(message)
    {
    }

    public override IList<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
}
