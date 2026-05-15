using CommonTestUtilities.Repositores;
using MyMusicLibrary.Application.UseCases.Music.Genre;
using MyMusicLibrary.Domain.Extensions;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;
using Shouldly;
using Xunit;

namespace UseCases.Test.Music.Genre;
public class GetGenreUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var useCase = CreateUseCase(genreNull: false);

        var result = await useCase.Execute();

        result.ShouldNotBeNull();
        result.Count.ShouldBe(3);
        result[0].ShouldNotBeNull();
    }

    [Fact]
    public async Task Error_Genres_NotFound()
    {
        var useCase = CreateUseCase(genreNull: true);

        var exception = await Should.ThrowAsync<NotFoundException>(useCase.Execute());

        exception.GetStatusCode().ShouldBe(System.Net.HttpStatusCode.NotFound);
        exception.GetErrorMessages().ShouldContain(ResourceMessagesException.GENRE_NOTFOUND);
    }

    private static GetGenreUseCase CreateUseCase(bool genreNull)
    {
        var repository = new MusicReadOnlyRepositoryBuilder();

        if (genreNull.IsFalse())
            repository.GetGenres();

        return new GetGenreUseCase(repository.Build());
    }
}
