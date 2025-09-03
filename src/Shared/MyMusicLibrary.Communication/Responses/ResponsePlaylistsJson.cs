namespace MyMusicLibrary.Communication.Responses;
public class ResponsePlaylistsJson
{
    public IList<ResponsePlaylistAllJson> Playlists { get; set; } = [];
}
