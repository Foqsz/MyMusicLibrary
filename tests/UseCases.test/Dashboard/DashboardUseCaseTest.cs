using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositores;
using MyMusicLibrary.Application.UseCases.Dashboard;
using MyMusicLibrary.Domain.Extensions;
using Shouldly;
using Xunit;

namespace UseCases.Test.Dashboard;
public class DashboardUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var user, var _) = UserBuilder.Build();

        var musics = MusicBuilder.Collection(user);

        var useCase = CreateUseCase(user, musics, false);

        var result = await useCase.Execute();

        result.ShouldNotBeNull();
        result.Musics.Count.ShouldBeGreaterThan(0); 

        foreach (var music in result.Musics)
        {
            music.Name.ShouldNotBeNullOrWhiteSpace();
            music.Album.ShouldNotBeNullOrWhiteSpace();
            music.Artist.ShouldNotBeNullOrWhiteSpace(); 
        }
    }

    [Fact]
    public async Task Error_Dashboard_Empty()
    {
        (var user, var _) = UserBuilder.Build();

        var musics = MusicBuilder.Collection(user);

        var useCase = CreateUseCase(user, musics, true);

        var result = await useCase.Execute();

        result.Musics.ShouldNotBeNull();
    }

    public static DashboardUseCase CreateUseCase(MyMusicLibrary.Domain.Entities.User user, IList<MyMusicLibrary.Domain.Entities.Music> musics, bool notfound)
    {
        var loggedUser = LoggedUserBuilder.Build(user);
        var repositoryOnly = new DashboardReadOnlyRepositoryBuilder();
        var mapper = MapperBuilder.Build();

        if (musics is not null && notfound.IsFalse())
            repositoryOnly.GetForDashboard(user, musics);

        return new DashboardUseCase(loggedUser, repositoryOnly.Build(), mapper);
    }
}
