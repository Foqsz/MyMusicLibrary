using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyMusicLibrary.Exceptions.ExceptionsBase;
public class ExistMusicException : MyMusicLibraryException
{
    public ExistMusicException(string message) : base(message)
    {
    }
    public override IList<string> GetErrorMessages() => [Message];
    public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
}
