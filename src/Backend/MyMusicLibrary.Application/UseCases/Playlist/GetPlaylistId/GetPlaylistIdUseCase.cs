using AutoMapper;
using MyMusicLibrary.Communication.Responses;
using MyMusicLibrary.Domain.Repositories.Playlist;
using MyMusicLibrary.Domain.Repositories.User;
using MyMusicLibrary.Domain.Services.LoggedUser;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;

namespace MyMusicLibrary.Application.UseCases.Playlist.GetPlaylistId;
public class GetPlaylistIdUseCase : IGetPlaylistIdUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IPlaylistReadOnlyRepository _playlistRepository;

    public GetPlaylistIdUseCase(ILoggedUser loggedUser,
        IPlaylistReadOnlyRepository repository,
        IUserReadOnlyRepository userRepository)
    {
        _loggedUser = loggedUser;
        _playlistRepository = repository;
    }

    public async Task<ResponsePlaylistAllJson?> Execute(long id)
    {
        var user = await _loggedUser.User();

        var playlist = await _playlistRepository.GetById(user, id);

        if (playlist is null)
            throw new PlaylistException(ResourceMessagesException.PLAYLIST_NOTFOUND);

        return new ResponsePlaylistAllJson()
        {
            Name = playlist.Name,
            Description = playlist.Description,
            OwnerName = user.Name,
            UserId = playlist.UserId,
            CreatedOn = playlist.CreatedOn
        };
    }
}
