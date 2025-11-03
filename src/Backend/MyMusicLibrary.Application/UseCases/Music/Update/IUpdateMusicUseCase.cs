using MyMusicLibrary.Communication.Request;
using MyMusicLibrary.Communication.Responses;

namespace MyMusicLibrary.Application.UseCases.Music.Update;
public interface IUpdateMusicUseCase
{
    Task<ResponseProfileMusicJson> Execute(RequestUpdateMusic request);
}
