using MyMusicLibrary.Communication.Responses;

namespace MyMusicLibrary.Application.UseCases.Music.GetById;
public interface IGetMusicByIdUseCase
{
    Task<ResponseRegisteredMusicJson> Execute(int id);   
}
