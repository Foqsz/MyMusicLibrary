using MyMusicLibrary.Domain.Extensions;
using MyMusicLibrary.Domain.Repositories.UnitOfWork;
using MyMusicLibrary.Domain.Repositories.User;
using MyMusicLibrary.Domain.Repositories.User.Delete;
using MyMusicLibrary.Domain.Services.LoggedUser;

namespace MyMusicLibrary.Application.UseCases.User.Delete;
public class DeleteUserAccountUseCase : IDeleteUserAccountUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUserDeleteAccountRepository _userDeleteAccountRepository;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserAccountUseCase(ILoggedUser loggedUser,
        IUserDeleteAccountRepository userDeleteAccountRepository,
        IUserReadOnlyRepository userReadOnlyRepository,
        IUnitOfWork unitOfWork)
    {
        _loggedUser = loggedUser;
        _userDeleteAccountRepository = userDeleteAccountRepository;
        _userReadOnlyRepository = userReadOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute()
    {
        var user = await _loggedUser.User();

        var userActive = await _userReadOnlyRepository.ExistActiveUserWithIdentifier(user.UserIdentifier);

        if (userActive.IsFalse())
            return;  

        await _userDeleteAccountRepository.DeleteAccount(user.UserIdentifier);

        await _unitOfWork.Commit();
    }
}
