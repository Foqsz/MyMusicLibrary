using MyMusicLibrary.Communication.Request;
using MyMusicLibrary.Communication.Responses;

namespace MyMusicLibrary.Application.UseCases.Music.Register;
public interface IRegisterMusicUseCase
{
    Task<ResponseRegisteredMusicJson> Execute(RequestMusicJson request);
}
