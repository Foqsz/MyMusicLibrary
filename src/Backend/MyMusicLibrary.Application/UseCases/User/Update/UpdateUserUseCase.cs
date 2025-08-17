using MyMusicLibrary.Communication.Request;
using MyMusicLibrary.Domain.Extensions;
using MyMusicLibrary.Domain.Repositories.UnitOfWork;
using MyMusicLibrary.Domain.Repositories.User;
using MyMusicLibrary.Domain.Repositories.User.Update;
using MyMusicLibrary.Domain.Services.LoggedUser;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;

namespace MyMusicLibrary.Application.UseCases.User.Update;
public class UpdateUserUseCase : IUpdateUserUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUpdateUserRepository _repositoryWriteOnly;
    private readonly IUserReadOnlyRepository _repositoryReadOnly;
    private readonly ILoggedUser _loggedUser;

    public UpdateUserUseCase(IUnitOfWork unitOfWork,
        IUpdateUserRepository repositoryWriteOnly,
        IUserReadOnlyRepository repositoryReadOnly,
        ILoggedUser loggedUser)
    {
        _unitOfWork = unitOfWork;
        _repositoryWriteOnly = repositoryWriteOnly;
        _repositoryReadOnly = repositoryReadOnly;
        _loggedUser = loggedUser;
    }

    public async Task Execute(RequestUpdateUserJson request)
    {
        var userLogged = await _loggedUser.User();

        await Validate(request, userLogged.Email);

        var user = await _repositoryReadOnly.GetById(userLogged.Id);

        user.Name = request.Name;
        user.Email = request.Email;

        _repositoryWriteOnly.Update(user);

        await _unitOfWork.Commit();
    }

    private async Task Validate(RequestUpdateUserJson request, string currentEmail)
    {
        var validator = new UpdateUserValidator();

        var result = await validator.ValidateAsync(request);

        if (result.IsValid && currentEmail.Equals(request.Email).IsFalse() && await _repositoryReadOnly.ExistActiveUserWithEmail(request.Email))
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(
                "email",
                ResourceMessagesException.EMAIL_ALREADY_REGISTERED
            ));
        }

        if (result.IsValid.IsFalse())
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
