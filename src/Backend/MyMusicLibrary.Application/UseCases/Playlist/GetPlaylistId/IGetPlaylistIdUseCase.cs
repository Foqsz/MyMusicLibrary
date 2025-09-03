using MyMusicLibrary.Communication.Responses;

namespace MyMusicLibrary.Application.UseCases.Playlist.GetPlaylistId;
public interface IGetPlaylistIdUseCase
{
    Task<ResponsePlaylistAllJson?> Execute(long id);
}
