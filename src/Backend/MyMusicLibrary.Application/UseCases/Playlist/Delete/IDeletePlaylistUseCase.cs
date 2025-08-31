namespace MyMusicLibrary.Application.UseCases.Playlist.Delete;
public interface IDeletePlaylistUseCase
{
    Task Execute(long playlistId);
}
