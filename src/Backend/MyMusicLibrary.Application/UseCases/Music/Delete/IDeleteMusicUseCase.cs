namespace MyMusicLibrary.Application.UseCases.Music.Delete;
public interface IDeleteMusicUseCase
{
    Task Execute(long id);
}
