using CommonTestUtilities.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyMusicLibrary.Infrastructure.DataAccess;


namespace WebApi.Test;
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private MyMusicLibrary.Domain.Entities.User _user = default!;
    private MyMusicLibrary.Domain.Entities.Music _music = default!;
    private MyMusicLibrary.Domain.Entities.Artist _artist = default!;
    private MyMusicLibrary.Domain.Entities.Playlist _playlist = default!;
    private MyMusicLibrary.Domain.Entities.UserFavoritesMusic _userMusicFavorites = default!;

    private string _password = string.Empty;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test")
            .ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<MyMusicLibraryDbContext>));
                if (descriptor is not null)
                    services.Remove(descriptor);

                var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                services.AddDbContext<MyMusicLibraryDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                    options.UseInternalServiceProvider(provider);
                });

                using var scope = services.BuildServiceProvider().CreateScope();

                var dbContext = scope.ServiceProvider.GetRequiredService<MyMusicLibraryDbContext>();

                dbContext.Database.EnsureDeleted();

                StartDataBase(dbContext);
            });
    }

    public string GetEmail() => _user.Email;
    public long GetUserId() => _user.Id;
    public string GetPassword() => _password;
    public string GetName() => _user.Name;
    public string GetMusicName() => _music.Name;    
    public string GetArtistName() => _artist.Name;
    public long GetMusicId() => _music.Id;
    public Guid GetUserIdentifier() => _user.UserIdentifier; 
    public long GetPlaylistId() => _playlist.Id;
    public string GetPlaylistName() => _playlist.Name;
    public string GetPlaylistDescription() => _playlist.Description;
    public long GetUserMusicFavoriteMusicId() => _userMusicFavorites.MusicId;
    public long GetUserMusicFavoriteUserId() => _userMusicFavorites.UserId;
    public bool GetUserActive() => _user.Active;

    private void StartDataBase(MyMusicLibraryDbContext dbContext)
    {
        (_user, _password) = UserBuilder.Build(); 
        _music = MusicBuilder.Build(_user); 
        _artist = ArtistBuilder.Builder(_user);
        _playlist = PlaylistBuilder.Build(_user);
        _userMusicFavorites = UserFavoritesMusicBuilder.Build(_user);

        _music.PlaylistId = _playlist.Id;

        dbContext.Users.Add(_user); 
        dbContext.Music.Add(_music); 
        dbContext.Artist.Add(_artist);
        dbContext.Playlist.Add(_playlist);
        dbContext.UserFavoritesMusic.Add(_userMusicFavorites);
        dbContext.SaveChanges();
    }

}
