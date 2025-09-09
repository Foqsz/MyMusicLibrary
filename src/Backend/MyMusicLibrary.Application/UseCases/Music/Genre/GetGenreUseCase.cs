using AutoMapper;
using MyMusicLibrary.Communication.Responses;
using MyMusicLibrary.Domain.Extensions;
using MyMusicLibrary.Domain.Repositories.Music;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;

namespace MyMusicLibrary.Application.UseCases.Music.Genre;
public class GetGenreUseCase : IGetGenreUseCase
{
    private readonly IMusicReadOnlyRepository _repositoryRead;
    private readonly IMapper _mapper;

    public GetGenreUseCase(IMusicReadOnlyRepository repositoryRead, IMapper mapper)
    {
        _repositoryRead = repositoryRead;
        _mapper = mapper;
    }

    public async Task<IList<ResponseGenreJson>> Execute()
    {
        var genres = await _repositoryRead.GetGenres();

        if (genres == null || genres.Any().IsFalse())
            throw new NotFoundException(ResourceMessagesException.GENRE_NOTFOUND);

        return genres.Select(g => new ResponseGenreJson
        {
            Genre = g.Genre,
            Count = g.Count
        }).ToList();
    }

}
