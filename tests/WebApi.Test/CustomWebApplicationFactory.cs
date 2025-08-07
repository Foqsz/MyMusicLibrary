using CommonTestUtilities.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyMusicLibrary.Infrastructure.DataAccess;


namespace WebApi.Test;
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private MyMusicLibrary.Domain.Entities.User _user = default!;
    private MyMusicLibrary.Domain.Entities.Music _music = default!;
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
    public string GetPassword() => _password;
    public string GetName() => _user.Name;
    public Guid GetUserIdentifier() => _user.UserIdentifier; 

    private void StartDataBase(MyMusicLibraryDbContext dbContext)
    {
        (_user, _password) = UserBuilder.Build(); 
        _music = MusicBuilder.Build(_user); 

        dbContext.Users.Add(_user); 
        dbContext.Music.Add(_music); 
        dbContext.SaveChanges();
    }

}
