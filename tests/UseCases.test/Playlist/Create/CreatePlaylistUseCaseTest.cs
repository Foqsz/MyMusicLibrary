using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositores;
using CommonTestUtilities.Requests;
using MyMusicLibrary.Application.UseCases.Playlist.Create;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;
using Shouldly;
using Xunit;

namespace UseCases.Test.Playlist.Create;
public class CreatePlaylistUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var user, var _) = UserBuilder.Build();

        var playlist = RequestFromPlaylistJsonBuilder.Build(); 

        var useCase = CreateUseCase(user, playlist.Name, playlist.Description);

        var result = await useCase.Execute(playlist);

        result.ShouldNotBeNull();
        result.Name.ShouldBe(playlist.Name);
    }

    [Fact]
    public async Task Error_NamePlaylist_Empty()
    {
        (var user, var _) = UserBuilder.Build();

        var playlist = RequestFromPlaylistJsonBuilder.Build();
        playlist.Name = string.Empty;

        var useCase = CreateUseCase(user, playlist.Name, playlist.Description);

        var exception = await Should.ThrowAsync<ErrorOnValidationException>(async () => await useCase.Execute(playlist));

        exception.GetStatusCode().ShouldBe(System.Net.HttpStatusCode.BadRequest);
        exception.GetErrorMessages().ShouldContain(ResourceMessagesException.PLAYLIST_NAME_EMPTY);
    }

    [Fact]
    public async Task Error_NamePlaylist_Long()
    {
        (var user, var _) = UserBuilder.Build();

        var playlist = RequestFromPlaylistJsonBuilder.Build();
        playlist.Name = new string('a', 101);  

        var useCase = CreateUseCase(user, playlist.Name, playlist.Description);

        var exception = await Should.ThrowAsync<ErrorOnValidationException>(async () => await useCase.Execute(playlist));

        exception.GetStatusCode().ShouldBe(System.Net.HttpStatusCode.BadRequest);
        exception.GetErrorMessages().ShouldContain(ResourceMessagesException.PLAYLIST_NAME_TOO_LONG);
    }

    [Fact]
    public async Task Error_Description_Long()
    {
        (var user, var _) = UserBuilder.Build();

        var playlist = RequestFromPlaylistJsonBuilder.Build();
        playlist.Description = new string('a', 501);

        var useCase = CreateUseCase(user, playlist.Name, playlist.Description);

        var exception = await Should.ThrowAsync<ErrorOnValidationException>(async () => await useCase.Execute(playlist));

        exception.GetStatusCode().ShouldBe(System.Net.HttpStatusCode.BadRequest);
        exception.GetErrorMessages().ShouldContain(ResourceMessagesException.PLAYLIST_DESCRIPTION_TOO_LONG);
    }

    private static CreatePlaylistUseCase CreateUseCase(MyMusicLibrary.Domain.Entities.User user, string? name, string? description)
    {
        var loggedUser = LoggedUserBuilder.Build(user);
        var mapper = MapperBuilder.Build();
        var repositoryWriteOnly = PlaylistWriteOnlyRepositoryBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();

        if (name != null || description != null)
        {
            repositoryWriteOnly.Create(user, new MyMusicLibrary.Domain.Entities.Playlist
            {
                Name = name!,
                Description = description!,
                UserId = user.Id
            });
        } 

        return new CreatePlaylistUseCase(loggedUser, mapper, repositoryWriteOnly, unitOfWork);
    }
}
