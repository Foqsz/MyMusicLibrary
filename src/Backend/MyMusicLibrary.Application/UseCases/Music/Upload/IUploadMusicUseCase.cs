using MyMusicLibrary.Communication.Request;

namespace MyMusicLibrary.Application.UseCases.Music.Upload;
public interface IUploadMusicUseCase
{
    Task<string> Execute(RequestUploadMusicFormData file);
}
