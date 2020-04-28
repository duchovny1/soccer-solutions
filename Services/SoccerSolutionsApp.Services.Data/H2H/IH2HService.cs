using System.Threading.Tasks;

namespace SoccerSolutionsApp.Services.Data.H2H
{
    public interface IH2HService
    {
        Task<int> HomeTeamWins(int hometeamId, int awayTeamId);

        Task<int> AwayTeamWins(int hometeamId, int awayTeamId);

        Task<int> Draws(int hometeamId, int awayTeamId);

        Task<int> TotalGoalsScored(int hometeamId, int awayTeamId);

        Task<int> TotalGoalsHomeTeamScored(int hometeamId, int awayTeamId);

        Task<int> TotalGoalsAwayTeamScored(int hometeamId, int awayTeamId);

        Task<int> TotalBothTeamsScoredInLast20Games(int teamId);

        Task<int> TotalOver2point5GoalsInLast20Games(int teamId);
    }
}
