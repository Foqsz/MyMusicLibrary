namespace MyMusicLibrary.Application.UseCases.User.DoLogin.External;
public interface IExternalLoginUseCase
{
    Task<string> Execute(string name, string email);
}
