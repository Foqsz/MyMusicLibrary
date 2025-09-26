namespace MyMusicLibrary.Domain.Entities;
public class Music : EntityBase
{ 
    public string Name { get; set; } = string.Empty;
    public string Album { get; set; } = string.Empty;
    public IList<Artist> Artist { get; set; } = [];
    public long UserId { get; set; }
    public long? PlaylistId { get; set; }
    public string MusicKey { get; set; } = string.Empty;
    public string AwsS3BucketName { get; set; } = string.Empty;
}
