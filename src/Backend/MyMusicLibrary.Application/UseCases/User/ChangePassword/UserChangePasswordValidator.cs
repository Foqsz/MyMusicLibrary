using FluentValidation;
using MyMusicLibrary.Application.SharedValidators;
using MyMusicLibrary.Communication.Request;

namespace MyMusicLibrary.Application.UseCases.User.ChangePassword;
public class UserChangePasswordValidator : AbstractValidator<RequestUserChangePassword>
{
    public UserChangePasswordValidator()
    {
        RuleFor(u => u.NewPassword)
            .SetValidator(new PasswordValidator<RequestUserChangePassword>());
    }
}
