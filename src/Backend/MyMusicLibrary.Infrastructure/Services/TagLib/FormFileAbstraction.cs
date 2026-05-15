using Microsoft.AspNetCore.Http;
using static TagLib.File;

namespace MyMusicLibrary.Infrastructure.Services.TagLib;
public class FormFileAbstraction : IFileAbstraction
{
    public string Name { get; }
    public Stream ReadStream { get; }
    public Stream WriteStream => throw new NotSupportedException();

    public FormFileAbstraction(IFormFile formFile)
    {
        Name = formFile.FileName;
        ReadStream = formFile.OpenReadStream();
    }

    public void CloseStream(Stream stream)
    {
        stream.Dispose();
    }
}
