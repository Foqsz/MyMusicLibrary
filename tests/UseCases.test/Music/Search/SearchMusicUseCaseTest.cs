using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositores;
using MyMusicLibrary.Application.UseCases.Music.Search;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;
using Shouldly;
using Xunit;

namespace UseCases.Test.Music.Search;
public class SearchMusicUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var user, var _) = UserBuilder.Build();

        var music = MusicBuilder.Build(user);

        var useCase = CreateUseCase(user, music);

        var response = await useCase.Execute(music.Name);

        response.ShouldNotBeNull();
        response.Musics.ShouldNotBeNull();
        response.Musics.Count.ShouldBeGreaterThan(0);
        response.Musics[0].Name.ShouldBe(music.Name);
    }

    [Fact]
    public async Task Error_Music_NotExist()
    {
        (var user, var _) = UserBuilder.Build();

        var music = MusicBuilder.Build(user);

        var useCase = CreateUseCase(user, music);

        Func<Task> act = async () =>
        {
            await useCase.Execute("musica");
        };

        var exception = await act.ShouldThrowAsync<ExistMusicException>();
        exception.Message.ShouldBe(ResourceMessagesException.MUSIC_EMPTY);
    }

    private static SearchMusicUseCase CreateUseCase(MyMusicLibrary.Domain.Entities.User user,
        MyMusicLibrary.Domain.Entities.Music music)
    {
        var loggedUser = LoggedUserBuilder.Build(user);
        var musicReadOnlyRepository = new MusicReadOnlyRepositoryBuilder();
        var mapper = MapperBuilder.Build();

        if (music is not null)
        {
            musicReadOnlyRepository.Search(user, music);
        }


        return new SearchMusicUseCase(loggedUser, musicReadOnlyRepository.Build(), mapper);
    }
}
