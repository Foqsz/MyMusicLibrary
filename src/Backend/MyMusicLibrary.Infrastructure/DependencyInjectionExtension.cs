using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyMusicLibrary.Domain.Repositories.UnitOfWork;
using MyMusicLibrary.Domain.Repositories.User;
using MyMusicLibrary.Domain.Security.Cryptography;
using MyMusicLibrary.Infrastructure.DataAccess;
using MyMusicLibrary.Infrastructure.DataAccess.Repositories;
using MyMusicLibrary.Infrastucture.Extensions;
using MyMusicLibrary.Infrastucture.Security.Cryptography;
using System.Reflection;

namespace MyMusicLibrary.Infrastructure;
public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddRepositories(services);
        AddPasswordsEncrpter(services);
        AddDbContext_MySqlServer(services, configuration);
        AddFluentMigrator_MySql(services, configuration);
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IUserWriteOnlyRepository, UserWriteOnlyRepository>();
        services.AddScoped<IUserReadOnlyRepository, UserReadOnlyRepository>();
    }

    private static void AddPasswordsEncrpter(IServiceCollection services)
    {
        services.AddScoped<IPasswordEncripter, BCryptNet>();
    }

    private static void AddDbContext_MySqlServer(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.ConnectionString();
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 35));

        services.AddDbContext<MyMusicLibraryDbContext>(dbContextOptions =>
        {
            dbContextOptions.UseMySql(connectionString, serverVersion);
        });
    }

    private static void AddFluentMigrator_MySql(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.ConnectionString();

        services.AddFluentMigratorCore().ConfigureRunner(options =>
        {
            options
            .AddMySql5()
            .WithGlobalConnectionString(connectionString)
            .ScanIn(Assembly.Load("MyMusicLibrary.Infrastructure")).For.All();
        });
    }
}
