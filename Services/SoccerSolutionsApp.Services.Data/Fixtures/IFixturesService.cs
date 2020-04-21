namespace SoccerSolutionsApp.Services.Data.Fixtures
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SoccerSolutionsApp.Web.ViewModels.Fixtures;
    using SoccerSolutionsApp.Web.ViewModels.Main;
    using SoccerSolutionsApp.Web.ViewModels.Predictions;

    public interface IFixturesService
    {
        void CreateAsync(ImportFixturesApi model);

        IEnumerable<FixtureViewModel> GetFixturesByDate(FixturesByDateInputModel model);

        Task<IEnumerable<FixturesListingViewModel>> GetNextFixturesByLeagueIdAsync(int leagueId, int? take = null);

        Task<IEnumerable<NextFixturesViewModel>> GetNexTFixturesForTeamByIdAsync(int teamId, int? take = null);

        IEnumerable<FixtureForLeagueDropDownModel> GetNextFixturesByLeagueIdAndDaysAsync(int leagueId, int days);

        Task<IEnumerable<PastFixturesViewModel>> GetPastFixturesForTeamByIdAsync(int teamId, int? take = null, int skip = 0);


        Task<int> CountPastFixturesAsync(int teamId);
    }
}
