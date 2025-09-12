using MyMusicLibrary.Communication.Responses;

namespace MyMusicLibrary.Application.UseCases.Playlist.GetPlaylistId;
public interface IGetPlaylistIdUseCase
{
    Task<ResponsePlaylistIdJson?> Execute(long id);
}
