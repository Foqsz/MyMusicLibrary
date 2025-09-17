using MyMusicLibrary.Communication.Responses;

namespace MyMusicLibrary.Application.UseCases.User.GetUserMusicFavorites;
public interface IGetUserMusicFavoritesUseCase
{
    Task<ResponseMusicsJson> Execute();
}
