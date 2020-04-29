namespace SoccerSolutionsApp.Services.Data.H2H
{
    using System.Threading.Tasks;

    using SoccerSolutionsApp.Web.ViewModels.H2H;

    public interface IH2HService
    {
        Task<H2HViewModel> PrepareViewModel(int hometeamId, int awayTeamId);

        Task<int> HomeTeamWins(int hometeamId, int awayTeamId);

        Task<int> AwayTeamWins(int hometeamId, int awayTeamId);

        Task<int> Draws(int hometeamId, int awayTeamId);

        Task<int> TotalGoalsScored(int hometeamId, int awayTeamId);

        Task<int> TotalGoalsHomeTeamScored(int hometeamId, int awayTeamId);

        Task<int> TotalGoalsAwayTeamScored(int hometeamId, int awayTeamId);

        Task<int> TotalBothTeamsScoredInLast20Games(int teamId);

        Task<int> TotalOver2point5GoalsInLast20Games(int teamId);

        Task<int> TotalGamesBetweenTwoTeams(int hometeamId, int awayTeamId);

        Task<double> HowOftenTeamWinsAsAHomeTeam(int hometeamId);

        Task<double> HowOftenTeamWinsAsAnAwayTeam(int awayteamId);


    }
}
