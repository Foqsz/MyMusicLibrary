using MyMusicLibrary.Communication.Responses;

namespace MyMusicLibrary.Application.UseCases.Music.Favorite;
public interface IFavoriteMusicUseCase
{
    Task<ResponseProfileMusicJson> Execute(long musicId);
}
