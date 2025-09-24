using Microsoft.IdentityModel.Tokens;
using MyMusicLibrary.Communication.Request;
using MyMusicLibrary.Domain.Extensions;
using MyMusicLibrary.Domain.Services.LoggedUser;
using MyMusicLibrary.Domain.Services.Storage.Aws;
using MyMusicLibrary.Exceptions;
using MyMusicLibrary.Exceptions.ExceptionsBase;

namespace MyMusicLibrary.Application.UseCases.Music.Upload;
public class UploadMusicUseCase : IUploadMusicUseCase
{
    private readonly IS3Service _s3Service;
    private readonly ILoggedUser _loggedUser;

    public UploadMusicUseCase(IS3Service s3Service, ILoggedUser loggedUser)
    {
        _s3Service = s3Service;
        _loggedUser = loggedUser;
    }

    public async Task<string> Execute(RequestUploadMusicFormData file)
    {
        var user = await _loggedUser.User();

        if (user.Active.IsFalse())
            throw new InvalidActionException(ResourceMessagesException.ERROR_USER_IS_INACTIVE);

        var upload = await _s3Service.UploadFileAsync(file.Music!);

        if(upload.IsNullOrEmpty())
            throw new InvalidActionException(ResourceMessagesException.ERROR_INVALID_FILE);

        return upload;
    }
}
