using AutoMapper;
using MyMusicLibrary.Communication.Responses;
using MyMusicLibrary.Domain.Repositories.Music;
using MyMusicLibrary.Domain.Services.LoggedUser;
using MyMusicLibrary.Domain.Services.Storage.Aws;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;

namespace MyMusicLibrary.Application.UseCases.Music.GetById;
public class GetMusicByIdUseCase : IGetMusicByIdUseCase
{
    private readonly IMusicReadOnlyRepository _repository;
    private readonly ILoggedUser _logged;
    private readonly IMapper _mapper;
    private readonly IS3Service _s3Service;

    public GetMusicByIdUseCase(IMusicReadOnlyRepository repository, 
        ILoggedUser logged, 
        IMapper mapper, 
        IS3Service s3Service)
    {
        _repository = repository;
        _logged = logged;
        _mapper = mapper;
        _s3Service = s3Service;
    }

    public async Task<ResponseProfileMusicJson> Execute(long id)
    {
        var user = await _logged.User();

        var music = await _repository.GetById(user, id);

        if (music is null)
            throw new NotFoundException(ResourceMessagesException.MUSIC_EMPTY);

        var musicUrl = await _s3Service.GetFileUrl(music.MusicKey!);

        if(musicUrl is null)
            throw new NotFoundException(ResourceMessagesException.MUSIC_URL_NOT_FOUND);

        var musicMapper = _mapper.Map<Domain.Entities.Music, ResponseProfileMusicJson>(music);

        musicMapper.UrlMusicS3 = musicUrl.url;

        return musicMapper;
    }
}
