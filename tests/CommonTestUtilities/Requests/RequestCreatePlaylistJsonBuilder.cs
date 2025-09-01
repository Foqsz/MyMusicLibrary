using Bogus;
using MyMusicLibrary.Communication.Request;

namespace CommonTestUtilities.Requests;
public class RequestCreatePlaylistJsonBuilder
{
    public static RequestCreatePlaylistJson Build()
    {
        return new Faker<RequestCreatePlaylistJson>() 
            .RuleFor(playlist => playlist.Name, (f) => f.Lorem.Word())
            .RuleFor(playlist => playlist.Description, (f) => f.Lorem.Sentence());
    }
}
