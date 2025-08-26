using MyMusicLibrary.Communication.Responses;

namespace MyMusicLibrary.Application.UseCases.Music.GetById;
public interface IGetMusicByIdUseCase
{
    Task<ResponseProfileMusicJson> Execute(int id);   
}
