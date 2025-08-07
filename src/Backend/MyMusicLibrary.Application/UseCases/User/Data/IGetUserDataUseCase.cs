using MyMusicLibrary.Communication.Responses;

namespace MyMusicLibrary.Application.UseCases.User.Data;
public interface IGetUserDataUseCase
{
    Task<ResponseDataUser> Execute();
}
