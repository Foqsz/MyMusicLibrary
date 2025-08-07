using Bogus;
using MyMusicLibrary.Domain.Entities;

namespace CommonTestUtilities.Entities;
public class MusicBuilder
{
    public static IList<Music> Collection(User user, uint count = 2)
    {
        var list = new List<Music>();

        if (count == 0)
            count = 1;

        var musicId = 1;

        for (int i = 0; i < count; i++)
        {
            var fakeMusic = Build(user);
            fakeMusic.Id = musicId++;

            list.Add(fakeMusic);
        }

        return list;
    }

    public static Music Build(User user)
    {
        return new Faker<Music>()
            .RuleFor(m => m.Id, _ => 1)
            .RuleFor(m => m.Name, f => f.Lorem.Sentence(2))
            .RuleFor(m => m.Album, f => f.Lorem.Word())
            .RuleFor(m => m.Artist, f => f.Make(1, () => new Artist
            {
                Id = 1,
                Name = f.Person.FullName,
                Genre = f.Lorem.Word(), 
                MusicId = 1
            }))
            .RuleFor(m => m.UserId, _ => user.Id);
    }
}
