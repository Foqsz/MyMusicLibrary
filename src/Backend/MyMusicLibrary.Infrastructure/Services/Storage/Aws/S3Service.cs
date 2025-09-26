using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using MyMusicLibrary.Domain.Dtos;

namespace MyMusicLibrary.Domain.Services.Storage.Aws;
public class S3Service : IS3Service
{
    private readonly IAmazonS3 _s3Client;
    private readonly string bucketName;

    public S3Service(IAmazonS3 s3Client, string bucketName)
    {
        this._s3Client = s3Client;
        this.bucketName = bucketName;
    }

    public async Task<S3FilesDto> UploadFileAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("File is empty.", nameof(file));

        if (!file.FileName.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase) ||
            file.ContentType != "audio/mpeg")
            throw new InvalidOperationException("Only .mp3 files are allowed.");

        var fileTransferUtility = new TransferUtility(_s3Client);
        var key = $"{Guid.NewGuid()}_{file.FileName}";

        await using (var stream = file.OpenReadStream())
        {
            var request = new TransferUtilityUploadRequest
            {
                InputStream = stream,
                Key = key,
                BucketName = bucketName,
                ContentType = file.ContentType,
            };

            await fileTransferUtility.UploadAsync(request);
        }

        return new S3FilesDto(key: key, bucketName: bucketName);
    }

    public async Task DeleteFile(string key)
    {
        var deleteObjectRequest = new DeleteObjectRequest
        {
            BucketName = bucketName,
            Key = key
        };

        await _s3Client.DeleteObjectAsync(deleteObjectRequest);
    }
}
