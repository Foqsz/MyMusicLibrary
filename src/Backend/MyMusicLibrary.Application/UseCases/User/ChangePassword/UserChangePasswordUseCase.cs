using AutoMapper;
using MyMusicLibrary.Communication.Request;
using MyMusicLibrary.Domain.Extensions;
using MyMusicLibrary.Domain.Repositories.UnitOfWork;
using MyMusicLibrary.Domain.Repositories.User;
using MyMusicLibrary.Domain.Security.Cryptography;
using MyMusicLibrary.Domain.Services.LoggedUser;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;

namespace MyMusicLibrary.Application.UseCases.User.ChangePassword;
public class UserChangePasswordUseCase : IUserChangePasswordUseCase
{
    private readonly ILoggedUser _loggedUser;  
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IUserReadOnlyRepository _readOnlyRepository;

    public UserChangePasswordUseCase(ILoggedUser loggedUser, 
        IUnitOfWork unitOfWork, 
        IPasswordEncripter passwordEncripter,
        IUserReadOnlyRepository readOnlyRepository)
    {
        _loggedUser = loggedUser;
        _unitOfWork = unitOfWork;
        _passwordEncripter = passwordEncripter;
        _readOnlyRepository = readOnlyRepository;
    }

    public async Task Execute(RequestUserChangePassword request)
    {
        await Validator(request);

        var loggedUser = await _loggedUser.User();

        var user = await _readOnlyRepository.GetById(loggedUser.Id);
         
        if (_passwordEncripter.IsValid(request.CurrentPassword, user.Password).IsFalse())
            throw new UserChangePasswordException(ResourceMessagesException.INCORRECT_CURRENT_PASSWORD_ERROR);
         
        if (_passwordEncripter.IsValid(request.NewPassword, user.Password))
            throw new UserChangePasswordException(ResourceMessagesException.SAME_PASSWORD_ERROR);
         
        var newPassword = _passwordEncripter.Encrypt(request.NewPassword);

        user.Password = newPassword; 

        await _unitOfWork.Commit();
    }

    private static async Task Validator(RequestUserChangePassword password)
    {
        var validator = new UserChangePasswordValidator();

        var result = await validator.ValidateAsync(password);

        if (result.IsValid.IsFalse())
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }   
    }
}
