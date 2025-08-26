using MyMusicLibrary.Domain.Dtos;

namespace MyMusicLibrary.Communication.Responses;
public class ResponseProfileArtistJson
{
    public string Name { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public IList<MusicDto> Music { get; set; } = null!;
}
