using MyMusicLibrary.Communication.Request;
using MyMusicLibrary.Communication.Responses;
using MyMusicLibrary.Domain.Repositories.Music;
using MyMusicLibrary.Domain.Repositories.UnitOfWork;
using MyMusicLibrary.Domain.Services.LoggedUser;
using MyMusicLibrary.Domain.Services.Storage.Aws;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;

namespace MyMusicLibrary.Application.UseCases.Music.Update;
public class UpdateMusicUseCase : IUpdateMusicUseCase
{
    private readonly IMusicWriteOnlyRepository _musicWriteOnlyRepository;
    private readonly ILoggedUser _loggedUser;
    private readonly IMusicReadOnlyRepository _musicReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IS3Service _s3Service;

    public UpdateMusicUseCase(IMusicWriteOnlyRepository musicWriteOnlyRepository,
        ILoggedUser loggedUser,
        IMusicReadOnlyRepository musicReadOnlyRepository,
        IUnitOfWork unitOfWork,
        IS3Service s3Service)
    {
        _musicWriteOnlyRepository = musicWriteOnlyRepository;
        _loggedUser = loggedUser;
        _musicReadOnlyRepository = musicReadOnlyRepository;
        _unitOfWork = unitOfWork;
        _s3Service = s3Service;
    }

    public async Task<ResponseProfileMusicJson> Execute(RequestUpdateMusic request)
    {
        var user = await _loggedUser.User();

        var music = await _musicReadOnlyRepository.GetById(user, request.MusicId);
        if (music is null)
            throw new NotFoundException(ResourceMessagesException.MUSIC_EMPTY);

        var oldKey = music.MusicKey!;
        var extension = Path.GetExtension(oldKey);  
        var folder = Path.GetDirectoryName(oldKey)?.Replace("\\", "/");
        var newKey = string.IsNullOrEmpty(folder)
            ? $"{request.Name}{extension}"
            : $"{folder}/{request.Name}{extension}";

        await _s3Service.RenameFileAsync(oldKey, newKey);

        music.Name = request.Name;
        music.MusicKey = newKey;

        await _musicWriteOnlyRepository.Update(user, music);
        await _unitOfWork.Commit();

        return new ResponseProfileMusicJson()
        {
            Name = music.Name
        };
    }

}
