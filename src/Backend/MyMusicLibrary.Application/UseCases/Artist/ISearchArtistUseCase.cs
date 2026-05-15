using MyMusicLibrary.Communication.Responses;

namespace MyMusicLibrary.Application.UseCases.Artist;
public interface ISearchArtistUseCase
{
    Task<IList<ResponseProfileArtistJson>> Execute(string name);
}
