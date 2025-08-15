using MyMusicLibrary.Communication.Request;

namespace MyMusicLibrary.Application.UseCases.User.Update;
public interface IUpdateUserUseCase
{
    Task Execute(RequestUpdateUserJson request);
}
