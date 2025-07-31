using Microsoft.Extensions.DependencyInjection;
using MyMusicLibrary.Application.Services.AutoMapper;
using MyMusicLibrary.Application.UseCases.User.DoLogin;
using MyMusicLibrary.Application.UseCases.User.Register;

namespace MyMusicLibrary.Application;
public static class DependencyInjectionExtension
{
    public static void AddAplication(this IServiceCollection services)
    {
        AddAutoMapper(services);
        AddUseCases(services);
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddScoped(option => new AutoMapper.MapperConfiguration(options =>
        {
            options.AddProfile(new AutoMappingProfile());
        }).CreateMapper());
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();  
    }  
}
