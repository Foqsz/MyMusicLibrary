using MyMusicLibrary.Communication.Request;
using MyMusicLibrary.Communication.Responses;

namespace MyMusicLibrary.Application.UseCases.Music.Register;
public interface IRegisterMusicUseCase
{
    Task<ResponseProfileMusicJson> Execute(RequestMusicJson request);
}
