namespace SoccerSolutionsApp.Services.Data.Fixtures
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SoccerSolutionsApp.Web.ViewModels.Fixtures;
    using SoccerSolutionsApp.Web.ViewModels.Main;

    public interface IFixturesService
    {
        void CreateAsync(ImportFixturesApi model);

        IEnumerable<FixtureViewModel> GetFixturesByDate(FixturesByDateInputModel model);

        Task<IEnumerable<FixturesListingViewModel>> GetNextFixturesByLeagueIdAsync(int leagueId);

        Task<IEnumerable<PastFixturesViewModel>> GetPastFixturesForTeamByIdAsync(int teamId, int? take = null, int skip = 0);

        Task<IEnumerable<NextFixturesViewModel>> GetNexTFixturesForTeamByIdAsync(int teamId, int? take = null);

        Task<int> CountPastFixturesAsync(int teamId);
    }
}
