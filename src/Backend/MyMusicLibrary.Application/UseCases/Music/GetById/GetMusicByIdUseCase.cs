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

    public GetMusicByIdUseCase(IMusicReadOnlyRepository repository, ILoggedUser logged)
    {
        _repository = repository;
        _logged = logged;
    }

    public async Task<ResponseRegisteredMusicJson> Execute(int id)
    {
        var user = await _logged.User();

        var music = await _repository.GetById(user, id);

        return music is null ? throw new NotFoundException(ResourceMessagesException.MUSIC_EMPTY) : new ResponseRegisteredMusicJson
        {
            Album = music.Album,
            Name = music.Name,
            Artist = string.Join(", ", music.Artist.Select(artist => artist.Name)),
        };
    }
}
