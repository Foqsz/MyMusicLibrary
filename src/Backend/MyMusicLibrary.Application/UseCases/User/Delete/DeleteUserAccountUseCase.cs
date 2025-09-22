using MyMusicLibrary.Domain.Extensions;
using MyMusicLibrary.Domain.Repositories.UnitOfWork;
using MyMusicLibrary.Domain.Repositories.User;
using MyMusicLibrary.Domain.Repositories.User.Delete;
using MyMusicLibrary.Domain.Services.LoggedUser;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;

namespace MyMusicLibrary.Application.UseCases.User.Delete;
public class DeleteUserAccountUseCase : IDeleteUserAccountUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUserDeleteAccountRepository _userDeleteAccountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserAccountUseCase(ILoggedUser loggedUser,
        IUserDeleteAccountRepository userDeleteAccountRepository,
        IUnitOfWork unitOfWork)
    {
        _loggedUser = loggedUser;
        _userDeleteAccountRepository = userDeleteAccountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute()
    {
        var user = await _loggedUser.User();

        if (user.Active.IsFalse())
            throw new InvalidActionException(ResourceMessagesException.ERROR_USER_IS_INACTIVE);

        await _userDeleteAccountRepository.DeleteAccount(user.UserIdentifier);

        await _unitOfWork.Commit();
    }
}
