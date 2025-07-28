using MyMusicLibrary.Communication.Responses;

namespace MyMusicLibrary.Application.UseCases.User.Register;
public class RegisterUseCase
{
    public ResponseRegisteredUserJson Execute (RequestRegisterUserJson request)
    {
        Validate


        return new ResponseRegisteredUserJson
        {
            Name = request.Name
        };
    }
}
