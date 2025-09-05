using AutoMapper;
using MyMusicLibrary.Communication.Responses;
using MyMusicLibrary.Domain.Extensions;
using MyMusicLibrary.Domain.Repositories.Playlist;
using MyMusicLibrary.Domain.Repositories.User;
using MyMusicLibrary.Domain.Services.LoggedUser;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;

namespace MyMusicLibrary.Application.UseCases.Playlist.GetPlaylistName;
public class GetPlaylistNameUseCase : IGetPlaylistNameUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IPlaylistReadOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly IUserReadOnlyRepository _repositoryUser;

    public GetPlaylistNameUseCase(ILoggedUser loggedUser, IPlaylistReadOnlyRepository repository, IMapper mapper, IUserReadOnlyRepository repositoryUser)
    {
        _loggedUser = loggedUser;
        _repository = repository;
        _mapper = mapper;
        _repositoryUser = repositoryUser;
    }

    public async Task<ResponsePlaylistsJson> Execute(string name)
    {
        var playlists = await _repository.GetByName(name);

        if (playlists is null || playlists.Any().IsFalse())
            throw new PlaylistException(ResourceMessagesException.PLAYLIST_NOTFOUND);

        var userIds = playlists.Select(p => p.UserId).Distinct();

        var owners = await _repositoryUser.GetByIds(userIds);
        var ownersDict = owners.ToDictionary(o => o.Id, o => o);

        var responseList = playlists.Select(p => new ResponsePlaylistAllJson
        {
            Name = p.Name,
            Description = p.Description,
            UserId = p.UserId,
            OwnerName = ownersDict.TryGetValue(p.UserId, out var owner) ? owner.Name : "Desconhecido",
            CreatedOn = p.CreatedOn
        }).ToList();

        return new ResponsePlaylistsJson
        {
            Playlists = responseList
        };
    }
}
