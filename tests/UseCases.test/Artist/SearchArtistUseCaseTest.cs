using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositores;
using MyMusicLibrary.Application.UseCases.Artist;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;
using Shouldly;
using Xunit;

namespace UseCases.Test.Artist;
public class SearchArtistUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var user, var _) = UserBuilder.Build();

        var artist = ArtistBuilder.Builder(user);

        var useCase = CreateUseCase(user, artist);

        var result = await useCase.Execute(artist.Name);

        result.ShouldNotBeNull();
        result.Count.ShouldBe(1);
        result[0].Name.ShouldBe(artist.Name);
    }

    [Fact]
    public async Task Error_Artist_Not_Exist()
    {
        (var user, var _) = UserBuilder.Build();

        var artist = ArtistBuilder.Builder(user);

        var useCase = CreateUseCase(user, artist);

        var exception = await Should.ThrowAsync<NotFoundException>(async () =>
        {
            await useCase.Execute("Osvaldo");
        });

        exception.Message.ShouldBe(ResourceMessagesException.ARTIST_NOTFOUND);
        exception.GetStatusCode().ShouldBe(System.Net.HttpStatusCode.NotFound);
        exception.GetErrorMessages().Count.ShouldBe(1);
    }

    private static SearchArtistUseCase CreateUseCase(MyMusicLibrary.Domain.Entities.User user, MyMusicLibrary.Domain.Entities.Artist artist)
    {
        var loggedUser = LoggedUserBuilder.Build(user);
        var repositoryRead = new ArtistReadOnlyRepositoryBuilder();
        var mapper = MapperBuilder.Build();

        if (artist is not null)
            repositoryRead.SearchArtist(user, artist);

        return new SearchArtistUseCase(loggedUser, repositoryRead.Build(), mapper);
    }
}
