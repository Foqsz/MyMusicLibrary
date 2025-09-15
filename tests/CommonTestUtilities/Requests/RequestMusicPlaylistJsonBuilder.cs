using Bogus;
using MyMusicLibrary.Communication.Request;

namespace CommonTestUtilities.Requests;
public class RequestMusicPlaylistJsonBuilder
{
    public static RequestMusicPlaylistJson Build()
    {
        return new RequestMusicPlaylistJson()
        {
            MusicId = 1,
            PlaylistId = 1,
        };
    }
}
