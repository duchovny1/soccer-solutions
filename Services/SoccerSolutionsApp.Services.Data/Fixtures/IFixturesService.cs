namespace SoccerSolutionsApp.Services.Data.Fixtures
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SoccerSolutionsApp.Web.ViewModels.Fixtures;
    using SoccerSolutionsApp.Web.ViewModels.Main;

    public interface IFixturesService
    {
        Task CreateAsync(ImportFixturesApi model);

        IEnumerable<FixtureViewModel> GetFixturesByDate(FixturesByDateInputModel model);

        Task<IEnumerable<FixturesListingViewModel>> GetNextFixturesByIdAsync(int leagueId);

        Task<IEnumerable<PastFixturesViewModel>> GetPastFixturesForTeamById(int teamId, int take);

        Task<IEnumerable<NextFixturesViewModel>> GetNexTFixturesForTeamByIdAsync(int teamId, int take);

    }
}
