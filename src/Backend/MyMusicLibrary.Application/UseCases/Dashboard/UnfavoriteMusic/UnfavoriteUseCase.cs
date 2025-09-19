
using MyMusicLibrary.Domain.Repositories.Music;
using MyMusicLibrary.Domain.Repositories.UnitOfWork;
using MyMusicLibrary.Domain.Services.LoggedUser;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;

namespace MyMusicLibrary.Application.UseCases.Dashboard.RemoveMusicFavorite;
public class UnfavoriteUseCase : IUnfavoriteUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IMusicReadOnlyRepository _repositoyRead;
    private readonly IMusicWriteOnlyRepository _repositoryWrite;
    private readonly IUnitOfWork _unitOfWork;

    public UnfavoriteUseCase(ILoggedUser loggedUser,
        IMusicReadOnlyRepository repositoyRead,
        IMusicWriteOnlyRepository repositoryWrite,
        IUnitOfWork unitOfWork)
    {
        _loggedUser = loggedUser;
        _repositoyRead = repositoyRead;
        _repositoryWrite = repositoryWrite;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(long favoriteMusicId)
    {
        var user = await _loggedUser.User();

        var musicFavorited = _repositoyRead.GetMusicFavoriteId(user, favoriteMusicId);

        if (musicFavorited is null)
            throw new NotFoundException(ResourceMessagesException.MUSIC_EMPTY);

        await _repositoryWrite.UnfavoriteMusic(favoriteMusicId);
        await _unitOfWork.Commit();
    }
}
