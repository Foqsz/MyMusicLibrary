using Moq;
using MyMusicLibrary.Domain.Entities;
using MyMusicLibrary.Domain.Repositories.User;

namespace CommonTestUtilities.Repositores;
public class UserReadOnlyRepositoryBuilder
{
    private readonly Mock<IUserReadOnlyRepository> _repository;

    public UserReadOnlyRepositoryBuilder() =>
       _repository = new Mock<IUserReadOnlyRepository>();

    public void ExistActiveUserWithEmail(string email)
    {
        _repository.Setup(repository => repository.ExistActiveUserWithEmail(email)).ReturnsAsync(true);
    }

    public void ExistActiveUserWithIdentifier(Guid userIdentifier)
    {
        _repository.Setup(repository => repository.ExistActiveUserWithIdentifier(userIdentifier)).ReturnsAsync(true);
    }

    public void GetByEmail(User user)
    {
        _repository.Setup(repository => repository.GetByEmail(user.Email)).ReturnsAsync(user);
    }

    public void GetById(User user)
    {
        _repository.Setup(repository => repository.GetById(user.Id)).ReturnsAsync(user);
    }

    public IUserReadOnlyRepository Build() => _repository.Object;
}
