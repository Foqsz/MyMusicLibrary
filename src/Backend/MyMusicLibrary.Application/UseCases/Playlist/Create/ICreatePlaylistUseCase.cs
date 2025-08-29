using MyMusicLibrary.Communication.Request;
using MyMusicLibrary.Communication.Responses;

namespace MyMusicLibrary.Application.UseCases.Playlist.Create;
public interface ICreatePlaylistUseCase
{
    Task<ResponsePlaylistJson> CreatePlaylist(RequestCreatePlaylistJson request);
}
