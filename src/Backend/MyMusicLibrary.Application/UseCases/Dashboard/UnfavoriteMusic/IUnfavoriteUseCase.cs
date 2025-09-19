namespace MyMusicLibrary.Application.UseCases.Dashboard.RemoveMusicFavorite;
public interface IUnfavoriteUseCase
{
    Task Execute(long favoriteMusicId);
}
