using Bogus;
using MyMusicLibrary.Communication.Request;

namespace CommonTestUtilities.Requests;
public class RequestMusicJsonBuilder
{
    public static RequestMusicJson Build()
    {
        return new Faker<RequestMusicJson>()
            .RuleFor(music => music.Name, f => f.Lorem.Word())
            .RuleFor(music => music.Album, f => f.Lorem.Word())
            .RuleFor(music => music.Artist, f =>
                Enumerable.Range(0, f.Random.Int(1, 3)) // gera de 1 a 3 artistas
                    .Select(_ => new RequestArtistJson
                    {
                        Name = f.Name.FullName(),
                        Genre = f.Music.Genre()
                    })
                    .ToList()
            );
    }
}
