using Bogus;
using MyMusicLibrary.Domain.Entities;

namespace CommonTestUtilities.Entities;
public class UserFavoritesMusicBuilder
{
    public static UserFavoritesMusic Build(User user)
    {
        return new Faker<UserFavoritesMusic>()
            .RuleFor(m => m.MusicId, _ => 1)
            .RuleFor(u => u.UserId, _ => user.Id);
    }
}
