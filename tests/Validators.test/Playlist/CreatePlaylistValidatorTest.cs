using CommonTestUtilities.Requests;
using MyMusicLibrary.Application.UseCases.Playlist.Create;
using MyMusicLibrary.Exceptions;
using Shouldly;
using Xunit;

namespace Validators.Test.Playlist;
public class CreatePlaylistValidatorTest
{
    [Fact]
    public void Success()
    {
        var validate = new CreatePlaylistValidator();

        var request = RequestCreatePlaylistJsonBuilder.Build();

        var result = validate.Validate(request);

        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void Error_NamePlaylist_Empty()
    {
        var validate = new CreatePlaylistValidator();

        var request = RequestCreatePlaylistJsonBuilder.Build();
        request.Name = string.Empty;

        var result = validate.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.ErrorMessage == ResourceMessagesException.PLAYLIST_NAME_EMPTY);
    }

    [Fact]
    public void Error_NamePlaylist_Long()
    {
        var validate = new CreatePlaylistValidator();

        var request = RequestCreatePlaylistJsonBuilder.Build();
        request.Name = new string('a', 101);

        var result = validate.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.ErrorMessage == ResourceMessagesException.PLAYLIST_NAME_TOO_LONG);
    }

    [Fact]
    public void Error_Description_Long()
    {
        var validate = new CreatePlaylistValidator();

        var request = RequestCreatePlaylistJsonBuilder.Build();
        request.Description = new string('a', 501);

        var result = validate.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.ErrorMessage == ResourceMessagesException.PLAYLIST_DESCRIPTION_TOO_LONG);
    }
}
