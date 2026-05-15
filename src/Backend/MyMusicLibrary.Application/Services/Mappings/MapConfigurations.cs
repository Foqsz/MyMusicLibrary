using Mapster;
using MyMusicLibrary.Communication.Request;
using MyMusicLibrary.Communication.Responses;

namespace MyMusicLibrary.Application.Services.Mappings;
public static class MapConfigurations
{
    public static void Configure()
    {
        TypeAdapterConfig<RequestFromPlaylistJson, Domain.Entities.Playlist>
            .NewConfig()
            .Ignore(dest => dest.UserId)
            .Ignore(dest => dest.Musics);

        TypeAdapterConfig<RequestFavoriteMusicJson, Domain.Entities.UserFavoritesMusic>.NewConfig();

        TypeAdapterConfig<Domain.Entities.Music, ResponseProfileMusicJson>
            .NewConfig()
            .Map(dest => dest.Artist, src => string.Join(", ", src.Artist.Select(a => a.Name)))
            .Map(dest => dest.Genre, src => string.Join(", ", src.Artist.Select(a => a.Genre)))
            .Map(dest => dest.MusicId, src => src.Id);
    }
}
