using MyMusicLibrary.Domain.Repositories.UnitOfWork;
using MyMusicLibrary.Domain.Repositories.User;
using MyMusicLibrary.Domain.Security.Tokens;

namespace MyMusicLibrary.Application.UseCases.User.DoLogin.External;
public class ExternalLoginUseCase : IExternalLoginUseCase
{
    private readonly IUserReadOnlyRepository _repository;
    private readonly IUserWriteOnlyRepository _repositoryWrite;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAccessTokenGenerator _accessTokenGenerator;

    public ExternalLoginUseCase(
        IUserReadOnlyRepository repository,
        IUserWriteOnlyRepository repositoryWrite,
        IUnitOfWork unitOfWork,
        IAccessTokenGenerator accessTokenGenerator)
    {
        _repository = repository;
        _repositoryWrite = repositoryWrite;
        _unitOfWork = unitOfWork;
        _accessTokenGenerator = accessTokenGenerator;
    }

    public async Task<string> Execute(string name, string email)
    {
        var user = await _repository.GetByEmail(email);

        if (user is null)
        {
            user = new Domain.Entities.User
            {
                Name = name,
                Email = email,
                UserIdentifier = Guid.NewGuid(),
                Password = "-"
            };

            await _repositoryWrite.Add(user);
            await _unitOfWork.Commit();
        }

        return _accessTokenGenerator.Generate(user.UserIdentifier);
    }
}
