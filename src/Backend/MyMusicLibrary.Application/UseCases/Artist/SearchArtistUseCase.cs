using AutoMapper;
using MyMusicLibrary.Communication.Responses;
using MyMusicLibrary.Domain.Repositories.Artist;
using MyMusicLibrary.Domain.Services.LoggedUser;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;

namespace MyMusicLibrary.Application.UseCases.Artist;
public class SearchArtistUseCase : ISearchArtistUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IArtistReadOnlyRepository _artistRepository;
    private readonly IMapper _mapper;
    public SearchArtistUseCase(ILoggedUser loggedUser, IArtistReadOnlyRepository artistRepository, IMapper mapper)
    {
        _loggedUser = loggedUser;
        _artistRepository = artistRepository;
        _mapper = mapper;
    }

    public async Task<IList<ResponseProfileArtistJson>> Execute(string name)
    {
        var user = await _loggedUser.User();

        var artists = await _artistRepository.SearchArtist(user, name);

        if (artists is null)
            throw new NotFoundException(ResourceMessagesException.ARTIST_NOTFOUND);

        var response = _mapper.Map<IList<ResponseProfileArtistJson>>(artists);

        return response;
    }
}
