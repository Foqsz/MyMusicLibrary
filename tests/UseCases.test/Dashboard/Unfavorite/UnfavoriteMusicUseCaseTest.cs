using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositores;
using MyMusicLibrary.Application.UseCases.Dashboard.RemoveMusicFavorite;
using MyMusicLibrary.Domain.Extensions;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace UseCases.Test.Dashboard.Unfavorite;
public class UnfavoriteMusicUseCaseTest
{
    [Fact]
    public void Success()
    {
        (var user, var _) = UserBuilder.Build();

        var music = MusicBuilder.Build(user);

        var useCase = CreateUseCase(user, music, false);

        var result = useCase.Execute(music.Id);

        result.Status.ShouldBe(TaskStatus.RanToCompletion);
        result.IsCompletedSuccessfully.ShouldBeTrue();
    }

    [Fact]
    public async Task Error_No_Favorite_Music()
    {
        (var user, var _) = UserBuilder.Build();

        var music = MusicBuilder.Build(user);

        var useCase = CreateUseCase(user, music, true);

        var result = await Should.ThrowAsync<NotFoundException>(() => useCase.Execute(music.Id));

        result.Message.ShouldBe(ResourceMessagesException.MUSIC_EMPTY);
    }


    private UnfavoriteUseCase CreateUseCase(MyMusicLibrary.Domain.Entities.User user, MyMusicLibrary.Domain.Entities.Music music, bool notfound)
    {
        var loggedUser = LoggedUserBuilder.Build(user);
        var repositoryRead = new MusicReadOnlyRepositoryBuilder();
        var repositoryWrite = MusicWriteOnlyRepositoryBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();

        if (music is not null && notfound.IsFalse())
        {
            repositoryRead.GetMusicFavoriteId(user, music);
            repositoryWrite.UnfavoriteMusic(music.Id);
        }

        return new UnfavoriteUseCase(loggedUser, repositoryRead.Build(), repositoryWrite, unitOfWork);
    }
}
