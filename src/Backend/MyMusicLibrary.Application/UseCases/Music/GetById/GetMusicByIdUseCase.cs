using AutoMapper;
using MyMusicLibrary.Communication.Responses;
using MyMusicLibrary.Domain.Repositories.Music;
using MyMusicLibrary.Domain.Services.LoggedUser;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;

namespace MyMusicLibrary.Application.UseCases.Music.GetById;
public class GetMusicByIdUseCase : IGetMusicByIdUseCase
{
    private readonly IMusicReadOnlyRepository _repository;
    private readonly ILoggedUser _logged;
    private readonly IMapper _mapper;

    public GetMusicByIdUseCase(IMusicReadOnlyRepository repository, ILoggedUser logged, IMapper mapper)
    {
        _repository = repository;
        _logged = logged;
        _mapper = mapper;
    }

    public async Task<ResponseProfileMusicJson> Execute(long id)
    {
        var user = await _logged.User();

        var music = await _repository.GetById(user, id);

        if (music is null)
            throw new NotFoundException(ResourceMessagesException.MUSIC_EMPTY);

        var musicMapper = _mapper.Map<Domain.Entities.Music, ResponseProfileMusicJson>(music);

        return musicMapper;
    }
}
