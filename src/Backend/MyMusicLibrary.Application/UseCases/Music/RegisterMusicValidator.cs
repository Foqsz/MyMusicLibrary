using FluentValidation;
using MyMusicLibrary.Application.SharedValidators;
using MyMusicLibrary.Communication.Request;
using MyMusicLibrary.Exceptions;

namespace MyMusicLibrary.Application.UseCases.Music;
public class RegisterMusicValidator : AbstractValidator<RequestMusicJson>
{
    public RegisterMusicValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(ResourceMessagesException.NAME_EMPTY);
        RuleFor(x => x.Album)
            .NotEmpty()
            .WithMessage(ResourceMessagesException.ALBUM_EMPTY);
        RuleFor(x => x.Artist)
            .NotEmpty()
            .WithMessage(ResourceMessagesException.ARTIST_EMPTY);
    }
}
