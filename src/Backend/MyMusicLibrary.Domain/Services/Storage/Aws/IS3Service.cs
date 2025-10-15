using Microsoft.AspNetCore.Http;
using MyMusicLibrary.Domain.Dtos;

namespace MyMusicLibrary.Domain.Services.Storage.Aws;
public interface IS3Service
{
    Task<S3FilesDto> UploadFileAsync(IFormFile file);
    Task DeleteFile(string key);
    Task<S3UrlDto> GetFileUrl(string key);
}
