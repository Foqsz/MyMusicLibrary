using AutoMapper;
using MyMusicLibrary.Communication.Request;
using MyMusicLibrary.Communication.Responses;
using MyMusicLibrary.Domain.Extensions;
using MyMusicLibrary.Domain.Repositories.Playlist;
using MyMusicLibrary.Domain.Repositories.UnitOfWork;
using MyMusicLibrary.Domain.Services.LoggedUser;
using MyMusicLibrary.Exceptions.ExceptionsBase;

namespace MyMusicLibrary.Application.UseCases.Playlist.Create;
public class CreatePlaylistUseCase : ICreatePlaylistUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IMapper _mapper;
    private readonly IPlaylistWriteOnlyRepository _repositoryWriteOnly;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePlaylistUseCase(ILoggedUser loggedUser, IMapper mapper, IPlaylistWriteOnlyRepository repositoryWriteOnly, IUnitOfWork unitOfWork)
    {
        _loggedUser = loggedUser;
        _mapper = mapper;
        _repositoryWriteOnly = repositoryWriteOnly;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponsePlaylistJson> Execute(RequestFromPlaylistJson request)
    {
        await Validate(request);

        var user = await _loggedUser.User();

        var playlist = _mapper.Map<Domain.Entities.Playlist>(request);
        playlist.UserId = user.Id;

        await _repositoryWriteOnly.Create(user, playlist);

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
