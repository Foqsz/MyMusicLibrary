
using MyMusicLibrary.Domain.Repositories.Music;
using MyMusicLibrary.Domain.Repositories.UnitOfWork;
using MyMusicLibrary.Domain.Services.LoggedUser;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;

namespace MyMusicLibrary.Application.UseCases.Music.Delete;
public class DeleteMusicUseCase : IDeleteMusicUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IMusicWriteOnlyRepository _musicWriteOnlyRepository;
    private readonly IMusicReadOnlyRepository _musicReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteMusicUseCase(ILoggedUser loggedUser, IMusicWriteOnlyRepository musicWriteOnlyRepository, IUnitOfWork unitOfWork, IMusicReadOnlyRepository musicReadOnlyRepository)
    {
        _loggedUser = loggedUser;
        _musicWriteOnlyRepository = musicWriteOnlyRepository;
        _unitOfWork = unitOfWork;
        _musicReadOnlyRepository = musicReadOnlyRepository;
    }

    public async Task Execute(long id)
    {
        var user = await _loggedUser.User();

        var musicaId = await _musicReadOnlyRepository.GetById(user, id);

        if (musicaId is null)
            throw new ExistMusicException(ResourceMessagesException.MUSIC_NOT_BELONG_TO_USER);

        var musicDelete = _musicWriteOnlyRepository.Delete(musicaId.Id);

        await _unitOfWork.Commit();
    }
}
