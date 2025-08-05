using MyMusicLibrary.Communication.Responses;

namespace MyMusicLibrary.Application.UseCases.DashBoard;
public interface IDashboardUseCase
{
    Task<ResponseMusicsJson> Execute();
}
