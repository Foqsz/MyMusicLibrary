using MyMusicLibrary.Communication.Responses;

namespace MyMusicLibrary.Application.UseCases.Music.Search;
public interface ISearchMusicUseCase
{
    Task<ResponseMusicsJson> Execute(string name);
}
