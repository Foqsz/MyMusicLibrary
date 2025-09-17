using Mapster;
using MyMusicLibrary.Communication.Responses;
using MyMusicLibrary.Domain.Extensions;
using MyMusicLibrary.Domain.Repositories.Music;
using MyMusicLibrary.Domain.Services.LoggedUser;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;

namespace MyMusicLibrary.Application.UseCases.User.GetUserMusicFavorites;
public class GetUserMusicFavoritesUseCase : IGetUserMusicFavoritesUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IMusicReadOnlyRepository _musicReadOnlyRepository;

    public GetUserMusicFavoritesUseCase(ILoggedUser loggedUser, 
        IMusicReadOnlyRepository musicReadOnlyRepository)
    {
        _loggedUser = loggedUser;
        _musicReadOnlyRepository = musicReadOnlyRepository;
    }

    public async Task<ResponseMusicsJson> Execute()
    {
        var user = await _loggedUser.User();

        var musicFavorites = await _musicReadOnlyRepository.GetUserMusicFavorites(user);

        if (musicFavorites is null || musicFavorites.Any().IsFalse())
            throw new ExistMusicException(ResourceMessagesException.ERROR_MUSIC_FAVORITES);

        var musicMapper = musicFavorites.Adapt<IList<ResponseProfileMusicJson>>();

        return new ResponseMusicsJson()
        {
            Musics = musicMapper
        };
    }
}
