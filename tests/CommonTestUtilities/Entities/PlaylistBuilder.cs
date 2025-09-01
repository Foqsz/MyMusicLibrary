using Bogus;
using MyMusicLibrary.Domain.Entities;

namespace CommonTestUtilities.Entities;
public class PlaylistBuilder
{
    public static Playlist Build(User user)
    {
        return new Faker<Playlist>()
            .RuleFor(p => p.Name, f => f.Lorem.Sentence(2))
            .RuleFor(p => p.Description, f => f.Lorem.Paragraph())
            .RuleFor(p => p.UserId, _ => user.Id);
    }
}
