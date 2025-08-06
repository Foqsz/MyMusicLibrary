using AutoMapper;
using MyMusicLibrary.Application.UseCases.DashBoard;
using MyMusicLibrary.Communication.Responses;
using MyMusicLibrary.Domain.Repositories.Dashboard;
using MyMusicLibrary.Domain.Services.LoggedUser;

namespace MyMusicLibrary.Application.UseCases.Dashboard;
public class DashboardUseCase : IDashboardUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IDashboardReadOnlyRepository _dashboardReadOnlyRepository;
    private readonly IMapper _mapper;

    public DashboardUseCase(ILoggedUser loggedUser, IDashboardReadOnlyRepository dashboardReadOnlyRepository, IMapper mapper)
    {
        _loggedUser = loggedUser;
        _dashboardReadOnlyRepository = dashboardReadOnlyRepository;
        _mapper = mapper;
    }

    public async Task<ResponseMusicsJson> Execute()
    {
        var userInfo = await _loggedUser.User();

        var musics = await _dashboardReadOnlyRepository.GetForDashboard(userInfo);

        var responseMusics = _mapper.Map<IList<ResponseRegisteredMusicJson>>(musics);

        return new ResponseMusicsJson
        {
            Musics = responseMusics
        };
    }
}
