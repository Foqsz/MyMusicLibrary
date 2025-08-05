namespace MyMusicLibrary.Application.UseCases.DashBoard;
public interface IDashBoardUseCase
{
    Task<Domain.Entities.Music> Musics();
}
