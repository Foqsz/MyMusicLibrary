using AutoMapper;
using MyMusicLibrary.Communication.Request;
using MyMusicLibrary.Communication.Responses;
using MyMusicLibrary.Domain.Repositories.Music;
using MyMusicLibrary.Domain.Repositories.UnitOfWork;
using MyMusicLibrary.Domain.Services.LoggedUser;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;

namespace MyMusicLibrary.Application.UseCases.Music.Register;
public class RegisterMusicUseCase : IRegisterMusicUseCase
{
    private readonly IMusicReadOnlyRepository _musicReadOnlyRepository;
    private readonly IMusicWriteOnlyRepository _musicWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public RegisterMusicUseCase(IMusicReadOnlyRepository musicReadOnlyRepository, IUnitOfWork unitOfWork, IMapper mapper, ILoggedUser loggedUser, IMusicWriteOnlyRepository musicWriteOnlyRepository)
    {
        _musicReadOnlyRepository = musicReadOnlyRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _loggedUser = loggedUser;
        _musicWriteOnlyRepository = musicWriteOnlyRepository;
    }

    public async Task<ResponseRegisteredMusicJson> Execute(RequestMusicJson request)
    {
        await Validate(request);

        var loggedUser = await _loggedUser.User();

        var music = _mapper.Map<Domain.Entities.Music>(request);
        music.UserId = loggedUser.Id;

        foreach (var artist in music.Artist)
        {
            artist.MusicId = music.Id;   // FK direta (se estiver usando)
            artist.Music = music;   // relacionamento inverso, se for bidirecional
        }


        var musicExist = await _musicReadOnlyRepository.ThereIsThisSong(loggedUser, music.Name, music.Album);

        if (musicExist)
        {
            throw new ExistMusicException(ResourceMessagesException.MUSIC_EXIST);
        }

        await _musicWriteOnlyRepository.Add(music);

        await _unitOfWork.Commit();

        return new ResponseRegisteredMusicJson
        {
            Name = music.Name,
            Album = music.Album,
            Artist = string.Join(", ", music.Artist.Select(artista => artista.Name))
        };
    }

    private static async Task Validate(RequestMusicJson request)
    {
        var validator = new RegisterMusicValidator();

        var result = await validator.ValidateAsync(request);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
