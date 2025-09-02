using MyMusicLibrary.Communication.Request;
using MyMusicLibrary.Communication.Responses;

namespace MyMusicLibrary.Application.UseCases.Playlist.Update;
public interface IUpdatePlaylistUseCase
{
    Task<ResponsePlaylistJson> Execute(long id, RequestFromPlaylistJson request);
}
