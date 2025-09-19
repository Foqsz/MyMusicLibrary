using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositores;
using MyMusicLibrary.Application.UseCases.Dashboard.GetUserMusicFavorites;
using MyMusicLibrary.Domain.Extensions;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;
using Shouldly;
using Xunit;

namespace UseCases.Test.Dashboard.GetUserMusicFavorites;
public class GetUserMusicFavoritesUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var user, var _) = UserBuilder.Build();
        var music = MusicBuilder.Collection(user);

        var useCase = CreateUseCase(user, music, false);

        var result = await useCase.Execute();

        result.Musics.ShouldNotBeNull();
        result.Musics.Count.ShouldBe(2);
        result.Musics[0].ShouldNotBeNull();
        result.Musics[1].ShouldNotBeNull();
    }

    [Fact]
    public async Task Error_Music_Favorites_NotFound()
    {
        (var user, var _) = UserBuilder.Build();
        var music = MusicBuilder.Collection(user);

        var useCase = CreateUseCase(user, music, true);

        var result = await Should.ThrowAsync<ExistMusicException>(async () => await useCase.Execute());

        result.Message.ShouldNotBeNull();
        result.Message.ShouldBe(ResourceMessagesException.ERROR_MUSIC_FAVORITES);
         
    }

    private static GetUserMusicFavoritesUseCase CreateUseCase(MyMusicLibrary.Domain.Entities.User user,
        IList<MyMusicLibrary.Domain.Entities.Music> music,
        bool notfound)
    {
        var loggedUser = LoggedUserBuilder.Build(user);
        var repositoryRead = new MusicReadOnlyRepositoryBuilder();

        if (music is not null && notfound.IsFalse())
            repositoryRead.GetUserMusicFavorites(user, music);

        return new GetUserMusicFavoritesUseCase(loggedUser, repositoryRead.Build());
    }
}
