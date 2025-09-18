using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositores;
using MyMusicLibrary.Application.UseCases.Music.Favorite;
using MyMusicLibrary.Domain.Extensions;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;
using Shouldly;
using Xunit;

namespace UseCases.Test.Music.Favorite;
public class FavoriteMusicUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var user, var _) = UserBuilder.Build();
        var music = MusicBuilder.Build(user);

        var useCase = CreateUseCase(user, music, false);

        var result = await useCase.Execute(music.Id);

        result.ShouldNotBeNull();
        result.Name.ShouldBe(music.Name);
        result.Album.ShouldBe(music.Album);
    }

    [Fact]
    public async Task Error_Music_NotFound()
    {
        (var user, var _) = UserBuilder.Build();
        var music = MusicBuilder.Build(user);

        var useCase = CreateUseCase(user, music, true);

        var result = await Should.ThrowAsync<ExistMusicException>(async () => await useCase.Execute(music.Id));

        result.Message.ShouldBe(ResourceMessagesException.MUSIC_EMPTY);
    }

    private static FavoriteMusicUseCase CreateUseCase(MyMusicLibrary.Domain.Entities.User user,
        MyMusicLibrary.Domain.Entities.Music music, 
        bool notfound)
    {
        var loggedUser = LoggedUserBuilder.Build(user);
        var musicWriteOnlyRepository = MusicWriteOnlyRepositoryBuilder.Build();
        var musicReadOnlyRepository = new MusicReadOnlyRepositoryBuilder();
        var unitOfWork = UnitOfWorkBuilder.Build();

        if (music is not null && notfound.IsFalse())
            musicReadOnlyRepository.GetById(user, music);

        return new FavoriteMusicUseCase(loggedUser, musicWriteOnlyRepository, musicReadOnlyRepository.Build(), unitOfWork);
    }
}
