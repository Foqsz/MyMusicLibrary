using FluentValidation;
using MyMusicLibrary.Application.SharedValidators;
using MyMusicLibrary.Communication.Request;
using MyMusicLibrary.Domain.Extensions;
using MyMusicLibrary.Exceptions;

namespace MyMusicLibrary.Application.UseCases.User.Register;
public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(ResourceMessagesException.NAME_EMPTY);
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(ResourceMessagesException.EMAIL_EMPTY);
        RuleFor(x => x.Password)
            .SetValidator(new PasswordValidator<RequestRegisterUserJson>());
        When(user => string.IsNullOrEmpty(user.Email).IsFalse(), () =>
        {
            RuleFor(user => user.Email).EmailAddress().WithMessage(ResourceMessagesException.EMAIL_INVALID);
        });
    }
} 
