using MyMusicLibrary.Communication.Responses;

namespace MyMusicLibrary.Application.UseCases.Playlist.GetPlaylistAll;
public interface IGetPlaylistAllUseCase
{
    Task<ResponsePlaylistsJson> Execute();
}
