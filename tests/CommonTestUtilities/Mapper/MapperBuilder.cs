using AutoMapper;
using MyMusicLibrary.Application.Services.AutoMapper;
using MyMusicLibrary.Application.Services.Mappings;

namespace CommonTestUtilities.Mapper;
public class MapperBuilder
{
    public static IMapper Build()
    {
        var mapper = new MapperConfiguration(options =>
        {
            options.AddProfile(new AutoMappingProfile());
        }).CreateMapper();

        return mapper;
    }

    public static void Mapster()
    {
        MapConfigurations.Configure();
    }
}
