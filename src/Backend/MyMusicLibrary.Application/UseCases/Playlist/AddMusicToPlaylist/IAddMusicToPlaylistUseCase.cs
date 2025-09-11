using MyMusicLibrary.Communication.Request;
using MyMusicLibrary.Communication.Responses;

namespace MyMusicLibrary.Application.UseCases.Playlist.AddMusicToPlaylist;
public interface IAddMusicToPlaylistUseCase
{
    Task<ResponseMusicPlaylistJson> Execute(RequestMusicPlaylistJson request);
}
