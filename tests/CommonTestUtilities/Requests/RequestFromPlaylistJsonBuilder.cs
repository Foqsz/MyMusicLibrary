using Bogus;
using MyMusicLibrary.Communication.Request;

namespace CommonTestUtilities.Requests;
public class RequestFromPlaylistJsonBuilder
{
    public static RequestFromPlaylistJson Build()
    {
        return new Faker<RequestFromPlaylistJson>() 
            .RuleFor(playlist => playlist.Name, (f) => f.Lorem.Word())
            .RuleFor(playlist => playlist.Description, (f) => f.Lorem.Sentence());
    }
}
