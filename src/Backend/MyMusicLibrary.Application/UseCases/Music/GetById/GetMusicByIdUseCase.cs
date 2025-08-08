using AutoMapper;
using MyMusicLibrary.Communication.Responses;
using MyMusicLibrary.Domain.Repositories.Music;
using MyMusicLibrary.Domain.Services.LoggedUser;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;
using System.Collections.Generic;

namespace MyMusicLibrary.Application.UseCases.Music.GetById;
public class GetMusicByIdUseCase : IGetMusicByIdUseCase
{
    private readonly IMusicReadOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _logged;

    public GetMusicByIdUseCase(IMusicReadOnlyRepository repository, IMapper mapper, ILoggedUser logged)
    {
        _repository = repository;
        _mapper = mapper;
        _logged = logged;
    }

    public async Task<ResponseRegisteredMusicJson> Execute(int id)
    {
        var user = await _logged.User();

        var music = await _repository.GetById(user, id);

        if (music is null)
            throw new NotFoundException(ResourceMessagesException.MUSIC_EMPTY); 

        return new ResponseRegisteredMusicJson
        {
            Album = music.Album,
            Name = music.Name,
            Artist = string.Join(", ", music.Artist.Select(artist => artist.Name)),
        };
    }
}
