
using MyMusicLibrary.Domain.Repositories.Playlist;
using MyMusicLibrary.Domain.Repositories.UnitOfWork;
using MyMusicLibrary.Domain.Services.LoggedUser;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;

namespace MyMusicLibrary.Application.UseCases.Playlist.Delete;
public class DeletePlaylistUseCase : IDeletePlaylistUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDeletePlaylistRepository _repository;
    private readonly IPlaylistReadOnlyRepository _readOnlyRepository;

    public DeletePlaylistUseCase(ILoggedUser loggedUser,
        IUnitOfWork unitOfWork,
        IDeletePlaylistRepository repository,
        IPlaylistReadOnlyRepository readOnlyRepository)
    {
        _loggedUser = loggedUser;
        _unitOfWork = unitOfWork;
        _repository = repository;
        _readOnlyRepository = readOnlyRepository;
    }

    public async Task Execute(long playlistId)
    {
        var user = await _loggedUser.User();

        var playlist = await _readOnlyRepository.GetById(user, playlistId);

        if (playlist is null)
            throw new PlaylistException(ResourceMessagesException.PLAYLIST_NOTFOUND);
        else
        {
            await _repository.Delete(user, playlistId);
            await _unitOfWork.Commit();
        }
    }
}
