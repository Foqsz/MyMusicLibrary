namespace MyMusicLibrary.Communication.Request;
public class RequestMusicJson
{
    public string Name { get; set; } = string.Empty;
    public string Album { get; set; } = string.Empty;
    public IList<RequestArtistJson> Artist { get; set; } = []; 
}
