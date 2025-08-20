using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositores;
using MyMusicLibrary.Application.UseCases.Music.Delete;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;
using Shouldly;
using Xunit;

namespace UseCases.Test.Music.Delete;
public class DeleteMusicUserUseCase
{
    [Fact]
    public async Task Success()
    {
        var (user, _) = UserBuilder.Build();
        var music = MusicBuilder.Build(user);

        var useCase = CreateUseCase(user, music);

        Func<Task> act = async () =>
        {
            await useCase.Execute(music.Id);
        };

        await act.ShouldNotThrowAsync();  
    }

    [Fact]
    public async Task Error_Music_Invalid_Id()
    {
        var (user, _) = UserBuilder.Build();
        var music = MusicBuilder.Build(user);

        var useCase = CreateUseCase(user, music);

        var exception = await Should.ThrowAsync<ExistMusicException>(async () =>
        {
            await useCase.Execute(123);
        });

        exception.Message.ShouldBe(ResourceMessagesException.MUSIC_NOT_BELONG_TO_USER); 
    }


    private static DeleteMusicUseCase CreateUseCase(MyMusicLibrary.Domain.Entities.User user, MyMusicLibrary.Domain.Entities.Music music)
    {
        var musicWriteRepository = MusicWriteOnlyRepositoryBuilder.Build();
        var userLogged = LoggedUserBuilder.Build(user);
        var unitOfWork = UnitOfWorkBuilder.Build();
        var musicReadRepository = new MusicReadOnlyRepositoryBuilder().GetById(user, music).Build();

        return new DeleteMusicUseCase(userLogged, musicWriteRepository, unitOfWork, musicReadRepository);
    }
}
