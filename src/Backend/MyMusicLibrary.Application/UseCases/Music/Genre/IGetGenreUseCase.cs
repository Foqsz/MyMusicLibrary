using MyMusicLibrary.Communication.Responses;

namespace MyMusicLibrary.Application.UseCases.Music.Genre;
public interface IGetGenreUseCase
{
    Task<IList<ResponseGenreJson>> Execute();
}
