using AutoMapper;
using MyMusicLibrary.Communication.Request;
using MyMusicLibrary.Communication.Responses;
using MyMusicLibrary.Domain.Dtos;

namespace MyMusicLibrary.Application.Services.AutoMapper;
public class AutoMappingProfile : Profile
{
    public AutoMappingProfile()
    {
        RequestDomain();
        DomainResponse();
    }

    private void RequestDomain()
    {
        CreateMap<RequestRegisterUserJson, Domain.Entities.User>()
            .ForMember(dest => dest.Password, opt => opt.Ignore());

        CreateMap<RequestMusicJson, Domain.Entities.Music>();

        CreateMap<RequestArtistJson, Domain.Entities.Artist>()
            .ForMember(dest => dest.Music, opt => opt.Ignore());

        CreateMap<RequestUpdateUserJson, Domain.Entities.User>();

        CreateMap<RequestFromPlaylistJson, Domain.Entities.Playlist>()
            .ForMember(dest => dest.UserId, opt => opt.Ignore());
    }

    private void DomainResponse()
    {
        CreateMap<Domain.Entities.Music, ResponseProfileMusicJson>()
            .ForMember(dest => dest.Artist, opt => opt.MapFrom(src =>
                string.Join(", ", src.Artist.Select(a => a.Name))));

        CreateMap<Domain.Entities.Music, MusicDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))  
            .ForMember(dest => dest.Album, opt => opt.MapFrom(src => src.Album));

        CreateMap<Domain.Entities.Artist, ResponseProfileArtistJson>()
            .ForMember(dest => dest.Music, opt => opt.MapFrom(src => src.Music != null
                ? new List<Domain.Entities.Music> { src.Music }
                : new List<Domain.Entities.Music>())); //se existe mais de uma musica, retorno uma lista, se nao, retorno uma lista vazia

        CreateMap<Domain.Entities.User, ResponseDataUser>().ReverseMap();

        CreateMap<Domain.Entities.Music, ResponseMusicsJson>()
            .ForMember(dest => dest.Musics, opt => opt.MapFrom(src =>
                string.Join(", ", src.Artist.Select(a => a.Name))));

        CreateMap<Domain.Entities.User, ResponseUpdateUserJson>().ReverseMap();

        CreateMap<Domain.Entities.Playlist, ResponsePlaylistJson>();
    }
}
