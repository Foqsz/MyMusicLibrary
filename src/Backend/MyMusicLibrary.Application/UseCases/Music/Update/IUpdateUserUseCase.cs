using MyMusicLibrary.Communication.Request;

namespace MyMusicLibrary.Application.UseCases.Music.Update;
public interface IUpdateUserUseCase
{
    Task Execute(RequestUpdateUserJson request);
}
