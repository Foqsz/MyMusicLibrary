using MyMusicLibrary.Communication.Request;

namespace MyMusicLibrary.Application.UseCases.User.ChangePassword;
public interface IUserChangePasswordUseCase
{
    Task Execute(RequestUserChangePassword request);
}
