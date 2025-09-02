using Microsoft.Extensions.DependencyInjection;
using MyMusicLibrary.Application.Services.AutoMapper;
using MyMusicLibrary.Application.UseCases.Artist;
using MyMusicLibrary.Application.UseCases.Dashboard;
using MyMusicLibrary.Application.UseCases.DashBoard;
using MyMusicLibrary.Application.UseCases.Music.Delete;
using MyMusicLibrary.Application.UseCases.Music.GetById;
using MyMusicLibrary.Application.UseCases.Music.Register;
using MyMusicLibrary.Application.UseCases.Music.Search;
using MyMusicLibrary.Application.UseCases.Playlist.Create;
using MyMusicLibrary.Application.UseCases.Playlist.Delete;
using MyMusicLibrary.Application.UseCases.Playlist.Update;
using MyMusicLibrary.Application.UseCases.User.ChangePassword;
using MyMusicLibrary.Application.UseCases.User.Data;
using MyMusicLibrary.Application.UseCases.User.Delete;
using MyMusicLibrary.Application.UseCases.User.DoLogin;
using MyMusicLibrary.Application.UseCases.User.DoLogin.External;
using MyMusicLibrary.Application.UseCases.User.Register;
using MyMusicLibrary.Application.UseCases.User.Update;

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
        services.AddScoped<IRegisterMusicUseCase, RegisterMusicUseCase>();
        services.AddScoped<IDashboardUseCase, DashboardUseCase>();
        services.AddScoped<IGetUserDataUseCase, GetUserDataUseCase>();
        services.AddScoped<IGetMusicByIdUseCase, GetMusicByIdUseCase>();
        services.AddScoped<IDeleteMusicUseCase, DeleteMusicUseCase>();
        services.AddScoped<IDeleteUserAccountUseCase, DeleteUserAccountUseCase>();
        services.AddScoped<IUpdateUserUseCase , UpdateUserUseCase>();
        services.AddScoped<IUserChangePasswordUseCase, UserChangePasswordUseCase>();
        services.AddScoped<IExternalLoginUseCase,  ExternalLoginUseCase>();
        services.AddScoped<ISearchMusicUseCase, SearchMusicUseCase>();
        services.AddScoped<ISearchArtistUseCase, SearchArtistUseCase>();
        services.AddScoped<ICreatePlaylistUseCase, CreatePlaylistUseCase>();
        services.AddScoped<IDeletePlaylistUseCase, DeletePlaylistUseCase>();
        services.AddScoped<IUpdatePlaylistUseCase, UpdatePlaylistUseCase>();
    }  
}
