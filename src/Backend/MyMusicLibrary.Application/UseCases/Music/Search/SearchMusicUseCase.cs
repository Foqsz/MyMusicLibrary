using AutoMapper;
using MyMusicLibrary.Communication.Responses;
using MyMusicLibrary.Domain.Repositories.Music;
using MyMusicLibrary.Domain.Services.LoggedUser;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;

namespace MyMusicLibrary.Application.UseCases.Music.Search;
public class SearchMusicUseCase : ISearchMusicUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IMusicReadOnlyRepository _musicReadOnlyRepository; 
    private readonly IMapper _mapper;

    public SearchMusicUseCase(ILoggedUser loggedUser, 
        IMusicReadOnlyRepository musicReadOnlyRepository, 
        IMapper mapper)
    {
        _loggedUser = loggedUser;
        _musicReadOnlyRepository = musicReadOnlyRepository; 
        _mapper = mapper;
    }
        
    public async Task<ResponseMusicsJson> Execute(string name)
    {
        var user = await _loggedUser.User();

        var searchMusic = await _musicReadOnlyRepository.Search(user, name);

        if (searchMusic is null)
            throw new ExistMusicException(ResourceMessagesException.MUSIC_EMPTY);

        var responseMusics = _mapper.Map<IList<ResponseRegisteredMusicJson>>(searchMusic);

        return new ResponseMusicsJson
        {
            Musics = responseMusics
        };
    }
}
