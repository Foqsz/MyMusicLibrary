using Bogus;
using MyMusicLibrary.Domain.Entities;

namespace CommonTestUtilities.Entities;
public class PlaylistBuilder
{
    public static IList<Playlist> Collection(User user, uint count = 2)
    {
        var list = new List<Playlist>();

        if (count == 0)
            count = 1;

        var playlistId = 1;

        for (int i = 0; i < count; i++)
        {
            var fakePlaylist = Build(user);
            fakePlaylist.Id = playlistId++;

            list.Add(fakePlaylist);
        }

        return list;
    }

    public static Playlist Build(User user)
    {
        return new Faker<Playlist>()
            .RuleFor(p => p.Name, f => f.Lorem.Sentence(2))
            .RuleFor(p => p.Description, f => f.Lorem.Paragraph())
            .RuleFor(p => p.CreatedOn, f => f.Date.Future())
            .RuleFor(p => p.UserId, _ => user.Id);
    }
}
