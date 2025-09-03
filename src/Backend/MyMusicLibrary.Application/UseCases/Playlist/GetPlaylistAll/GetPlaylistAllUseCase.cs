using AutoMapper;
using MyMusicLibrary.Communication.Responses;
using MyMusicLibrary.Domain.Extensions;
using MyMusicLibrary.Domain.Repositories.Playlist;
using MyMusicLibrary.Domain.Repositories.User;
using MyMusicLibrary.Domain.Services.LoggedUser;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;

namespace MyMusicLibrary.Application.UseCases.Playlist.GetPlaylistAll;
public class GetPlaylistAllUseCase : IGetPlaylistAllUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IPlaylistReadOnlyRepository _repositoryPlaylist;
    private readonly IUserReadOnlyRepository _userRepository;
    private readonly IMapper _mapper;

    public GetPlaylistAllUseCase(ILoggedUser loggedUser,
        IPlaylistReadOnlyRepository repository,
        IMapper mapper,
        IUserReadOnlyRepository userRepository)
    {
        _loggedUser = loggedUser;
        _repositoryPlaylist = repository;
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<ResponsePlaylistsJson> Execute()
    {
        var user = await _loggedUser.User();
        var userPlaylists = await _repositoryPlaylist.GetAll(user);

        if (userPlaylists.Any().IsFalse())
            throw new PlaylistException(ResourceMessagesException.PLAYLISTS_ALL_NOTFOUND);

        var playlists = new List<ResponsePlaylistAllJson>();

        foreach (var playlist in userPlaylists)
        {
            var owner = await _userRepository.GetById(playlist.UserId);

            playlists.Add(new ResponsePlaylistAllJson
            {
                Name = playlist.Name,
                Description = playlist.Description,
                UserId = playlist.UserId,
                OwnerName = owner.Name,
                CreatedOn = playlist.CreatedOn
            });
        }

        return new ResponsePlaylistsJson
        {
            Playlists = playlists
        };
    }
}
