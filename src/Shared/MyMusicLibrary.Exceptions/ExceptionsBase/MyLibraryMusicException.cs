using System.Net;

namespace MyMusicLibrary.Exceptions.ExceptionsBase;
public abstract class MyMusicLibraryException : SystemException
{
    protected MyMusicLibraryException(string message) : base(message) { }

    public abstract IList<string> GetErrorMessages();
    public abstract HttpStatusCode GetStatusCode();
}
