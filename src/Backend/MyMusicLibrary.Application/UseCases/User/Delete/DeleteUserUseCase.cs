using MyMusicLibrary.Domain.Extensions;
using MyMusicLibrary.Domain.Repositories.UnitOfWork;
using MyMusicLibrary.Domain.Repositories.User;
using MyMusicLibrary.Domain.Services.LoggedUser;

namespace MyMusicLibrary.Application.UseCases.User.Delete;
public class DeleteUserUseCase : IDeleteUserUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserUseCase(ILoggedUser loggedUser,
        IUserWriteOnlyRepository userWriteOnlyRepository,
        IUserReadOnlyRepository userReadOnlyRepository,
        IUnitOfWork unitOfWork)
    {
        _loggedUser = loggedUser;
        _userWriteOnlyRepository = userWriteOnlyRepository;
        _userReadOnlyRepository = userReadOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute()
    {
        var user = _loggedUser.User();

        var userActive = await _userReadOnlyRepository.ExistActiveUserWithIdentifier(user.Result.UserIdentifier);

        if (userActive.IsFalse())
            return;

        var userIdentifier = await _userReadOnlyRepository.GetById(user.Result.Id);

        await _userWriteOnlyRepository.DeleteAccount(userIdentifier.UserIdentifier);

        await _unitOfWork.Commit();
    }
}
