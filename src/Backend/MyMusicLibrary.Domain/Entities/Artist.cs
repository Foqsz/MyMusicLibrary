namespace MyMusicLibrary.Domain.Entities;
public class Artist : EntityBase
{ 
    public string Name { get; set; } = string.Empty;
    public string? Genre { get; set; } = string.Empty;
    public Music? Music { get; set; }
    public long? MusicId { get; set; }
}
