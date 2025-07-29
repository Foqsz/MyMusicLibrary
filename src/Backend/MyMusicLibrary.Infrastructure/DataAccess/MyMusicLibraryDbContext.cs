using Microsoft.EntityFrameworkCore;
using MyMusicLibrary.Domain.Entities;

namespace MyMusicLibrary.Infrastructure.DataAccess;
public class MyMusicLibraryDbContext : DbContext
{
    public MyMusicLibraryDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyMusicLibraryDbContext).Assembly);
    }
}
