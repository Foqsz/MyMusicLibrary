using AutoMapper;
using MyMusicLibrary.Application.UseCases.Playlist.Create;
using MyMusicLibrary.Communication.Request;
using MyMusicLibrary.Communication.Responses;
using MyMusicLibrary.Domain.Extensions;
using MyMusicLibrary.Domain.Repositories.Playlist;
using MyMusicLibrary.Domain.Repositories.UnitOfWork;
using MyMusicLibrary.Domain.Services.LoggedUser;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;

namespace MyMusicLibrary.Application.UseCases.Playlist.Update;
public class UpdatePlaylistUseCase : IUpdatePlaylistUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IMapper _mapper; 
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPlaylistReadOnlyRepository _repositoryReadOnly;
    private readonly IPlaylistWriteOnlyRepository _repositoryWriteOnly;

    public UpdatePlaylistUseCase(ILoggedUser loggedUser, 
        IMapper mapper, 
        IUnitOfWork unitOfWork, 
        IPlaylistReadOnlyRepository repositoryReadOnly, 
        IPlaylistWriteOnlyRepository repositoryWriteOnly)
    {
        _loggedUser = loggedUser;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _repositoryReadOnly = repositoryReadOnly;
        _repositoryWriteOnly = repositoryWriteOnly;
    }

    public async Task<ResponsePlaylistJson> Execute(long id, RequestFromPlaylistJson request)
    {
        await Validate(request);

        var user = await _loggedUser.User();

        var playlist = await _repositoryReadOnly.GetById(user, id);

        if (playlist is null)
            throw new PlaylistException(ResourceMessagesException.PLAYLIST_NOTFOUND);

        if (string.Equals(playlist.Name?.Trim(), request.Name?.Trim(), StringComparison.OrdinalIgnoreCase)
            && string.Equals(playlist.Description?.Trim(), request.Description?.Trim(), StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidUpdateException(ResourceMessagesException.UPDATE_ERROR);
        }

        playlist.Name = request.Name;
        playlist.Description = request.Description;

        _repositoryWriteOnly.Update(user, playlist);
        await _unitOfWork.Commit();

        return new ResponsePlaylistJson()
        {
            Name = playlist.Name,
            Description = playlist.Description
        };
    }

    private static async Task Validate(RequestFromPlaylistJson request)
    {
        var validator = new CreatePlaylistValidator();

        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.IsValid.IsFalse())
        {
            var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
