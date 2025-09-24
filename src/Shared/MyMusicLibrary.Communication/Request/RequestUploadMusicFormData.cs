using Microsoft.AspNetCore.Http;

namespace MyMusicLibrary.Communication.Request;
public class RequestUploadMusicFormData
{
    public IFormFile? Music { get; set; }
}
