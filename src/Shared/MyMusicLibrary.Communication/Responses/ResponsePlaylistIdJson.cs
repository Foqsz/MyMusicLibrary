using MyMusicLibrary.Domain.Entities;

namespace MyMusicLibrary.Communication.Responses;
public class ResponsePlaylistIdJson
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string OwnerName { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
    public IList<Music> Musics { get; set; } = [];
}
