namespace MyMusicLibrary.Domain.Entities;
public class UserFavoritesMusic : EntityBase
{
    public long UserId { get; set; }
    public long MusicId { get; set; }

}
