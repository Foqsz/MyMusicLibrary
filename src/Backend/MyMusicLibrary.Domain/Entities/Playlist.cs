namespace MyMusicLibrary.Domain.Entities;
public class Playlist : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty; 
    public long UserId { get; set; }
    public IList<Music> Musics { get; set; } = [];
}
