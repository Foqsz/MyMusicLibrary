using Microsoft.AspNetCore.Http;

namespace MyMusicLibrary.Domain.Services.Storage.Aws;
public interface IS3Service
{
    Task<string> UploadFileAsync(IFormFile file);
}
