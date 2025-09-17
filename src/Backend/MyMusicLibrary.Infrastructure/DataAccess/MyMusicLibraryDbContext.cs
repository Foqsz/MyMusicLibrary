using Microsoft.EntityFrameworkCore;
using MyMusicLibrary.Domain.Entities;

namespace MyMusicLibrary.Infrastructure.DataAccess;
public class MyMusicLibraryDbContext : DbContext
{
    public MyMusicLibraryDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Music> Music { get; set; }
    public DbSet<Artist> Artist { get; set; }
    public DbSet<Playlist> Playlist { get; set; }
    public DbSet<UserFavoritesMusic> UserFavoritesMusic { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyMusicLibraryDbContext).Assembly);
    }
}
