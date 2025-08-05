using AutoMapper;
using MyMusicLibrary.Communication.Request;

namespace MyMusicLibrary.Application.Services.AutoMapper;
public class AutoMappingProfile : Profile
{
    public AutoMappingProfile()
    {
        RequestDomain();
    }

    private void RequestDomain()
    {
        CreateMap<RequestRegisterUserJson, Domain.Entities.User>()
            .ForMember(dest => dest.Password, opt => opt.Ignore());

        CreateMap<RequestMusicJson, Domain.Entities.Music>();

        CreateMap<RequestArtistJson, Domain.Entities.Artist>()
            .ForMember(dest => dest.Music, opt => opt.Ignore());   
    }
}
