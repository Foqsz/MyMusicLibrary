using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositores;
using CommonTestUtilities.Requests;
using MyMusicLibrary.Application.UseCases.Music.Register;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;
using Shouldly;
using Xunit;

namespace UseCases.Test.Music.Register;
public class RegisterMusicUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var user, var _) = UserBuilder.Build(); 

        var music = RequestMusicJsonBuilder.Build();

        var useCase = CreateUseCase(user, music.Name, music.Album, musicExists: false);

        var result = await useCase.Execute(music);

        result.ShouldNotBeNull();
        result.Name.ShouldBe(music.Name);
        result.Album.ShouldBe(music.Album); 
    }

    [Fact]
    public async Task Error_Musics_Exists()
    {
        (var user, var _) = UserBuilder.Build();
        var music = RequestMusicJsonBuilder.Build();

        var useCase = CreateUseCase(user, music.Name, music.Album, musicExists: true);

        Func<Task> act = async () => await useCase.Execute(music);
        var exception = await act.ShouldThrowAsync<ExistMusicException>();

        exception.Message.ShouldBe(ResourceMessagesException.MUSIC_EXIST);
    }

    [Fact]
    public async Task Error_Name_Empty()
    {
        (var user, var _) = UserBuilder.Build();

        var music = RequestMusicJsonBuilder.Build();
        music.Name = string.Empty;

        var useCase = CreateUseCase(user, music.Name, music.Album, musicExists: false);

        Func<Task> act = async () => await useCase.Execute(music);
        var exception = await act.ShouldThrowAsync<ErrorOnValidationException>();

        exception.Message.ShouldBe(ResourceMessagesException.NAME_EMPTY);
    }

    [Fact]
    public async Task Error_Album_Empty()
    {
        (var user, var _) = UserBuilder.Build();

        var music = RequestMusicJsonBuilder.Build();
        music.Album = string.Empty;

        var useCase = CreateUseCase(user, music.Name, music.Album, musicExists: false);

        Func<Task> act = async () => await useCase.Execute(music);
        var exception = await act.ShouldThrowAsync<ErrorOnValidationException>();

        exception.Message.ShouldBe(ResourceMessagesException.ALBUM_EMPTY);
    }

    private static RegisterMusicUseCase CreateUseCase(MyMusicLibrary.Domain.Entities.User user, string? musicName = null, string? album = null, bool musicExists = false)
    {
        var repositoryReadOnly = new MusicReadOnlyRepositoryBuilder();
        var repositoryWriteOnly = MusicWriteOnlyRepositoryBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var mapper = MapperBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        if (musicName is not null && musicExists == true)
            repositoryReadOnly.ThereIsThisSong(user);

        return new RegisterMusicUseCase(repositoryReadOnly.Build(), unitOfWork, mapper, loggedUser, repositoryWriteOnly);
    }
}
