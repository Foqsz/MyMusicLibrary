using Microsoft.AspNetCore.Http;

namespace MyMusicLibrary.Domain.Services.Storage.Aws;
public interface IS3Service
{
    Task<S3FilesDto> UploadFileAsync(IFormFile file);
}
