
using MyMusicLibrary.Domain.Repositories.Music;
using MyMusicLibrary.Domain.Repositories.UnitOfWork;
using MyMusicLibrary.Domain.Services.LoggedUser;
using MyMusicLibrary.Domain.Services.Storage.Aws;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;

namespace MyMusicLibrary.Application.UseCases.Music.Delete;
public class DeleteMusicUseCase : IDeleteMusicUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IMusicWriteOnlyRepository _musicWriteOnlyRepository;
    private readonly IMusicReadOnlyRepository _musicReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IS3Service _s3Service;

    public DeleteMusicUseCase(ILoggedUser loggedUser,
        IMusicWriteOnlyRepository musicWriteOnlyRepository,
        IUnitOfWork unitOfWork,
        IMusicReadOnlyRepository musicReadOnlyRepository,
        IS3Service s3Service)
    {
        _loggedUser = loggedUser;
        _musicWriteOnlyRepository = musicWriteOnlyRepository;
        _unitOfWork = unitOfWork;
        _musicReadOnlyRepository = musicReadOnlyRepository;
        _s3Service = s3Service;
    }

    public async Task Execute(long id)
    {
        var user = await _loggedUser.User();

        var musicaId = await _musicReadOnlyRepository.GetById(user, id) ?? throw new ExistMusicException(ResourceMessagesException.MUSIC_NOT_BELONG_TO_USER);
        
        await _musicWriteOnlyRepository.Delete(musicaId.Id);
        await _s3Service.DeleteFile(musicaId.MusicKey!);
        await _unitOfWork.Commit();
    }
}
