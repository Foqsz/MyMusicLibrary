using AutoMapper;
using MyMusicLibrary.Communication.Request;
using MyMusicLibrary.Communication.Responses;

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
    }

    private void DomainResponse()
    {
        CreateMap<Domain.Entities.Music, ResponseRegisteredMusicJson>()
            .ForMember(dest => dest.Artist, opt => opt.MapFrom(src =>
                string.Join(", ", src.Artist.Select(a => a.Name))));

        CreateMap<Domain.Entities.User, ResponseDataUser>().ReverseMap();

        CreateMap<Domain.Entities.Music, ResponseMusicsJson>()
            .ForMember(dest => dest.Musics, opt => opt.MapFrom(src =>
                string.Join(", ", src.Artist.Select(a => a.Name))));
    }
}
