using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositores;
using MyMusicLibrary.Application.UseCases.Dashboard;
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

        var useCase = CreateUseCase(user, musics);

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

    public static DashboardUseCase CreateUseCase(MyMusicLibrary.Domain.Entities.User user, IList<MyMusicLibrary.Domain.Entities.Music> musics)
    {
        var loggedUser = LoggedUserBuilder.Build(user);
        var repositoryOnly = new DashboardReadOnlyRepositoryBuilder().GetForDashboard(user, musics).Build();
        var mapper = MapperBuilder.Build();

        return new DashboardUseCase(loggedUser, repositoryOnly, mapper);
    }
}
