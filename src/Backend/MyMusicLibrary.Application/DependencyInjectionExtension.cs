using Microsoft.Extensions.DependencyInjection;
using MyMusicLibrary.Application.Services.AutoMapper;
using MyMusicLibrary.Application.Services.Mappings;
using MyMusicLibrary.Application.UseCases.Artist;
using MyMusicLibrary.Application.UseCases.Dashboard;
using MyMusicLibrary.Application.UseCases.Dashboard.GetUserMusicFavorites;
using MyMusicLibrary.Application.UseCases.Dashboard.RemoveMusicFavorite;
using MyMusicLibrary.Application.UseCases.DashBoard;
using MyMusicLibrary.Application.UseCases.Music.Delete;
using MyMusicLibrary.Application.UseCases.Music.Favorite;
using MyMusicLibrary.Application.UseCases.Music.Genre;
using MyMusicLibrary.Application.UseCases.Music.GetById;
using MyMusicLibrary.Application.UseCases.Music.Register;
using MyMusicLibrary.Application.UseCases.Music.Search;
using MyMusicLibrary.Application.UseCases.Music.Upload;
using MyMusicLibrary.Application.UseCases.Playlist.AddMusicToPlaylist;
using MyMusicLibrary.Application.UseCases.Playlist.Create;
using MyMusicLibrary.Application.UseCases.Playlist.Delete;
using MyMusicLibrary.Application.UseCases.Playlist.GetPlaylistAll;
using MyMusicLibrary.Application.UseCases.Playlist.GetPlaylistId;
using MyMusicLibrary.Application.UseCases.Playlist.GetPlaylistName;
using MyMusicLibrary.Application.UseCases.Playlist.RemoveMusicFromPlaylist;
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
        AddMapping(services);
        AddUseCases(services);
    }

    private static void AddMapping(IServiceCollection services)
    {
        services.AddScoped(option => new AutoMapper.MapperConfiguration(options =>
        {
            options.AddProfile(new AutoMappingProfile());
        }).CreateMapper());

        MapConfigurations.Configure();
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
        services.AddScoped<IGetPlaylistIdUseCase, GetPlaylistIdUseCase>();
        services.AddScoped<IGetPlaylistAllUseCase, GetPlaylistAllUseCase>();
        services.AddScoped<IGetPlaylistNameUseCase, GetPlaylistNameUseCase>();
        services.AddScoped<IGetGenreUseCase, GetGenreUseCase>();
        services.AddScoped<IAddMusicToPlaylistUseCase, AddMusicToPlaylistUseCase>();
        services.AddScoped<IRemoveMusicFromPlaylistUseCase, RemoveMusicFromPlaylistUseCase>();
        services.AddScoped<IFavoriteMusicUseCase,  FavoriteMusicUseCase>();
        services.AddScoped<IGetUserMusicFavoritesUseCase, GetUserMusicFavoritesUseCase>();
        services.AddScoped<IUnfavoriteUseCase, UnfavoriteUseCase>();
        services.AddScoped<IUploadMusicUseCase, UploadMusicUseCase>();
    }  
}
