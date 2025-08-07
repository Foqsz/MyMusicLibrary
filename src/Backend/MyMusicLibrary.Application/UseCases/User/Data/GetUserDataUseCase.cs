using AutoMapper;
using MyMusicLibrary.Communication.Responses;
using MyMusicLibrary.Domain.Services.LoggedUser;

namespace MyMusicLibrary.Application.UseCases.User.Data;
public class GetUserDataUseCase : IGetUserDataUseCase
{
    private readonly ILoggedUser _loggedUser; 
    private readonly IMapper _mapper; 

    public GetUserDataUseCase(ILoggedUser loggedUser, IMapper mapper)
    {
        _loggedUser = loggedUser;
        _mapper = mapper; 
    }

    public async Task<ResponseDataUser> Execute()
    {
        var user = await _loggedUser.User(); 

        var response = _mapper.Map<ResponseDataUser>(user);

        return new ResponseDataUser
        {
            Name = response.Name,
            Email = response.Email,
            CreatedOn = response.CreatedOn
        };
    }
}
