using FluentValidation;
using MyMusicLibrary.Communication.Request;
using MyMusicLibrary.Exceptions;

namespace MyMusicLibrary.Application.UseCases.Playlist.Create;
public class CreatePlaylistValidator : AbstractValidator<RequestCreatePlaylistJson>
{
    public CreatePlaylistValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage(ResourceMessagesException.PLAYLIST_NAME_EMPTY)
            .MaximumLength(100)
            .WithMessage(ResourceMessagesException.PLAYLIST_NAME_TOO_LONG);

        RuleFor(p => p.Description)
            .MaximumLength(500)
            .WithMessage(ResourceMessagesException.PLAYLIST_DESCRIPTION_TOO_LONG);
    }
}
