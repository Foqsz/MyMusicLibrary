using MyMusicLibrary.Communication.Responses;

namespace MyMusicLibrary.Application.UseCases.Playlist.GetPlaylistName;
public interface IGetPlaylistNameUseCase
{
    Task<ResponsePlaylistsJson> Execute(string name);
}
