namespace SoccerSolutionsApp.Services.Data.TeamStatistics
{
    using SoccerSolutionsApp.Services.Data.Teams.ImportStatistics;

    public interface ITeamStatisticsService
    {
        bool AddStatistics(ImportStatisticsApi inputModel, int teamId, int leagueId);

        bool Update(ImportStatisticsApi inputModel, int teamId, int leagueId);
    }
}
