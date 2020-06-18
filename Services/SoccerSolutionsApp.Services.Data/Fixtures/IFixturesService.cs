namespace SoccerSolutionsApp.Services.Data.Fixtures
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SoccerSolutionsApp.Web.ViewModels.Fixtures;
    using SoccerSolutionsApp.Web.ViewModels.H2H;
    using SoccerSolutionsApp.Web.ViewModels.Main;
    using SoccerSolutionsApp.Web.ViewModels.Predictions;
    using SoccerSolutionsApp.Web.ViewModels.Teams;

    public interface IFixturesService
    {
        int Create(ImportApi model);

        Task<int> CountPastFixturesAsync(int teamId);

        Task<IEnumerable<FixtureViewModel>> GetFixturesByDate(FixturesByDateInputModel model);

        Task<IEnumerable<FixturesListingViewModel>> GetNextFixturesByLeagueIdAsync(int leagueId, int? take = null);

        Task<IEnumerable<NextFixturesViewModel>> GetNexTFixturesForTeamByIdAsync(int teamId, int? take = null);

        IEnumerable<FixtureForLeagueDropDownModel> GetNextFixturesByLeagueIdAndDaysAsync(int leagueId, int days);

        Task<IEnumerable<PastFixturesViewModel>> GetPastFixturesForTeamByIdAsync(int teamId, int? take = null, int skip = 0);

        Task<IEnumerable<FixturesListingViewModel>> GetFixtureForDate(int leagueId, DateTime date);

        Task<IEnumerable<PastFixturesViewModel>> GetHead2Head(int hometeamId, int awayteamId);

        Task<IEnumerable<PastFixturesViewModel>> GetFixturesForLeagueId(int leagueId);

        Task<FixtureDetailViewModel> GetSingleFixtureById(int fixtureId);

        Task<H2HTeamsInfoViewModel> GetH2HTeamsInfo(int fixtureId);

        Task<IEnumerable<PastFixturesViewModel>> GetAllPastFixturesByTeamId(int teamId, int? take = null, int skip = 0);

        Task<IEnumerable<NextFixturesViewModel>> GetAllNextFixtures(int teamId);

        Task<IEnumerable<PastFixturesViewModel>> GetAllPastWhereTeamIsHome(int teamId);

        Task<IEnumerable<PastFixturesViewModel>> GetAllPastWhereTeamIsAway(int teamId);

        Task<IEnumerable<PastFixturesViewModel>> GetAllPastForTeamAndLeague(int teamId, int leagueId);


    }
}
