using MyMusicLibrary.Communication.Responses;

namespace MyMusicLibrary.Application.UseCases.Dashboard.GetUserMusicFavorites;
public interface IGetUserMusicFavoritesUseCase
{
    Task<ResponseMusicsJson> Execute();
}
