using Mapster;
using MyMusicLibrary.Communication.Request;
using MyMusicLibrary.Communication.Responses;
using MyMusicLibrary.Domain.Repositories.Music;
using MyMusicLibrary.Domain.Repositories.UnitOfWork;
using MyMusicLibrary.Domain.Services.LoggedUser;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;

namespace MyMusicLibrary.Application.UseCases.Music.Favorite;
public class FavoriteMusicUseCase : IFavoriteMusicUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IMusicWriteOnlyRepository _musicWriteOnlyRepository;
    private readonly IMusicReadOnlyRepository _musicReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public FavoriteMusicUseCase(ILoggedUser loggedUser, 
        IMusicWriteOnlyRepository musicWriteOnlyRepository, 
        IMusicReadOnlyRepository musicReadOnlyRepository, 
        IUnitOfWork unitOfWork)
    {
        _loggedUser = loggedUser;
        _musicWriteOnlyRepository = musicWriteOnlyRepository;
        _musicReadOnlyRepository = musicReadOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseProfileMusicJson> Execute(long id)
    {
        var user = await _loggedUser.User();

        var music = await _musicReadOnlyRepository.GetById(user, id) ?? throw new ExistMusicException(ResourceMessagesException.MUSIC_EMPTY);

        var request = new RequestFavoriteMusicJson()
        {
            UserId = user.Id,
            MusicId = music.Id
        };

        var requestMapping = request.Adapt<Domain.Entities.UserFavoritesMusic>();

        await _musicWriteOnlyRepository.AddMusicFavorite(requestMapping);
        await _unitOfWork.Commit();

        return new ResponseProfileMusicJson()
        {
            Name = music.Name,
            Album = music.Album,
        };
    }
}
