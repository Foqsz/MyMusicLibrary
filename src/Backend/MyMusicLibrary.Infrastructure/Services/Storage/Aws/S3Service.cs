using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using MyMusicLibrary.Domain.Repositories.Music;

namespace MyMusicLibrary.Domain.Services.Storage.Aws;
public class S3Service : IS3Service
{
    private readonly IAmazonS3 s3Client;
    private readonly string bucketName;
    private readonly IMusicWriteOnlyRepository _musicWriteOnlyRepository;

    public S3Service(IAmazonS3 s3Client, string bucketName, IMusicWriteOnlyRepository musicWriteOnlyRepository)
    {
        this.s3Client = s3Client;
        this.bucketName = bucketName;
        _musicWriteOnlyRepository = musicWriteOnlyRepository;
    }

    public async Task<string> UploadFileAsync(IFormFile file)
    {
        if (!file.FileName.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase) || file.ContentType != "audio/mpeg")
            return string.Empty;

        var fileTransferUtility = new TransferUtility(s3Client);
        var key = $"{Guid.NewGuid()}_{file.FileName}";

        using (var stream = file.OpenReadStream())
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

        return key;
    }
}
