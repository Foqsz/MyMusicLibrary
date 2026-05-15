namespace MyMusicLibrary.Communication.Responses;
public class ResponseProfileMusicJson
{
    public string Name { get; set; } = string.Empty;
    public string Album { get; set; } = string.Empty;
    public string Artist { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public long MusicId { get; set; }
    public string UrlMusicS3 { get; set; } = string.Empty;
}
