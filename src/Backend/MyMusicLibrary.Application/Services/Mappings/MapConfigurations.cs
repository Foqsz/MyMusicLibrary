using Mapster;
using MyMusicLibrary.Communication.Request;

namespace MyMusicLibrary.Application.Services.Mappings;
public static class MapConfigurations
{
    public static void Configure()
    {
        TypeAdapterConfig<RequestFromPlaylistJson, Domain.Entities.Playlist>
            .NewConfig()
            .Ignore(dest => dest.UserId)
            .Ignore(dest => dest.Musics);
    }
}
