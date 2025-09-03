namespace MyMusicLibrary.Communication.Responses;
public class ResponsePlaylistAllJson
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string OwnerName { get; set; } = string.Empty;
    public long UserId { get; set; }
    public DateTime CreatedOn { get; set; }
}
