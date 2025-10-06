using Microsoft.AspNetCore.Http;
using MyMusicLibrary.Communication.Request;
using MyMusicLibrary.Domain.Extensions;
using MyMusicLibrary.Domain.Repositories.Artist;
using MyMusicLibrary.Domain.Repositories.Music;
using MyMusicLibrary.Domain.Repositories.UnitOfWork;
using MyMusicLibrary.Domain.Services.LoggedUser;
using MyMusicLibrary.Domain.Services.Storage.Aws;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;
using MyMusicLibrary.Infrastructure.Services.TagLib;

namespace MyMusicLibrary.Application.UseCases.Music.Upload;
public class UploadMusicUseCase : IUploadMusicUseCase
{
    private readonly IS3Service _s3Service;
    private readonly ILoggedUser _loggedUser;
    private readonly IMusicWriteOnlyRepository _musicWriteOnlyRepository;
    private readonly IUnitOfWork _UnitOfWork;
    private readonly IArtistReadOnlyRepository _artistReadOnly;

    public UploadMusicUseCase(IS3Service s3Service,
        ILoggedUser loggedUser,
        IMusicWriteOnlyRepository musicWriteOnlyRepository,
        IUnitOfWork unitOfWork,
        IArtistReadOnlyRepository artistReadOnly)
    {
        _s3Service = s3Service;
        _loggedUser = loggedUser;
        _musicWriteOnlyRepository = musicWriteOnlyRepository;
        _UnitOfWork = unitOfWork;
        _artistReadOnly = artistReadOnly;
    }

    public async Task<string> Execute(RequestUploadMusicFormData file)
    {
        var user = await _loggedUser.User();

        if (user.Active.IsFalse())
            throw new InvalidActionException(ResourceMessagesException.ERROR_USER_IS_INACTIVE);

        var upload = await _s3Service.UploadFileAsync(file.Music!);

        if (upload.key is null || upload.bucketName is null)
            throw new InvalidActionException(ResourceMessagesException.ERROR_INVALID_FILE);

        var musicName = Path.GetFileNameWithoutExtension(file.Music!.FileName).Replace(" ", "_");

        var artistName = ExtractArtistFromFile(file.Music) ?? "Desconhecido";

        var existingArtist = await _artistReadOnly.SearchArtist(user, artistName) ?? new List<Domain.Entities.Artist>();

        List<Domain.Entities.Artist> artists;
        if (existingArtist != null && existingArtist.Count > 0)
        {
            artists = existingArtist.ToList();
        }
        else
        {
            artists = new List<Domain.Entities.Artist> { new Domain.Entities.Artist { Name = artistName } };
        }

        var musicDbUpdate = new Domain.Entities.Music
        {
            UserId = user.Id,
            Name = musicName,
            MusicKey = upload.key,
            AwsS3BucketName = upload.bucketName,
            Artist = artists
        };

        await _musicWriteOnlyRepository.Add(musicDbUpdate);
        await _UnitOfWork.Commit();

        return upload.key;
    }

    private static string? ExtractArtistFromFile(IFormFile file)
    {
        var fileAbstraction = new FormFileAbstraction(file);
        var tfile = TagLib.File.Create(fileAbstraction);
        var artist = tfile.Tag.FirstPerformer; // pega o artista do arquivo
        return string.IsNullOrWhiteSpace(artist) ? null : artist;
    }

}
