using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositores;
using MyMusicLibrary.Application.UseCases.Music.GetById;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;
using Shouldly;
using Xunit;

namespace UseCases.Test.Music.GetMusic;
public class GetMusicByIdUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var user, var _) = UserBuilder.Build();

        var music = MusicBuilder.Build(user);

        var useCase = CreateUseCase(user, music);

        Func<Task> act = async () =>
        {
            await useCase.Execute(id: music.Id);
        };

        await act.ShouldNotThrowAsync();
        act.ShouldNotBeNull(); 
    }

    [Fact]
    public async Task Error_MusicId_NotFound()
    {
        (var user, var _) = UserBuilder.Build();

        var music = MusicBuilder.Build(user);

        var useCase = CreateUseCase(user, music);

        Func<Task> act = async () =>
        {
            await useCase.Execute(id: 2);
        };

        var exception = await act.ShouldThrowAsync<NotFoundException>();
        exception.Message.ShouldBe(ResourceMessagesException.MUSIC_EMPTY);
    }


    private static GetMusicByIdUseCase CreateUseCase(MyMusicLibrary.Domain.Entities.User? user, MyMusicLibrary.Domain.Entities.Music? music = null)
    {
        var repositoryReadOnly = new MusicReadOnlyRepositoryBuilder();
        var loggedUser = LoggedUserBuilder.Build(user!);
        var mapper = MapperBuilder.Build();

        if (music != null)
            repositoryReadOnly.GetById(user!, music);


        return new GetMusicByIdUseCase(repositoryReadOnly.Build(), loggedUser, mapper); 
    }
}
