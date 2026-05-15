namespace MyMusicLibrary.Communication.Request;
public class RequestUpdateMusic
{
    public long MusicId { get; set; }
    public string Name { get; set; } = string.Empty;
}
