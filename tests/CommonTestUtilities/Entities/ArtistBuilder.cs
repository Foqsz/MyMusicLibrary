using Bogus;
using MyMusicLibrary.Domain.Entities;

namespace CommonTestUtilities.Entities;
public class ArtistBuilder
{
    public static Artist Builder(User user)
    {
        return new Faker<Artist>()
            .RuleFor(a => a.Name, f => f.Person.FullName)
            .RuleFor(a => a.Genre, f => f.Lorem.Word())
            .RuleFor(a => a.Music, f => MusicBuilder.Build(user))
            .RuleFor(a => a.MusicId, _ => 1);
    }
}
