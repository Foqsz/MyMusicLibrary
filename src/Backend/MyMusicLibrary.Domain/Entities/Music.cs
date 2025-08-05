namespace MyMusicLibrary.Domain.Entities;
public class Music : EntityBase
{ 
    public string Name { get; set; } = string.Empty;
    public string Album { get; set; } = string.Empty;
    public IList<Artist> Artist { get; set; } = [];
    public long UserId { get; set; }
}
