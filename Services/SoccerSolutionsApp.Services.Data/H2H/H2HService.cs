namespace SoccerSolutionsApp.Services.Data.H2H
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SoccerSolutionsApp.Data.Common.Repositories;
    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Data.Models.Enums;
    using SoccerSolutionsApp.Web.ViewModels.H2H;

    public class H2HService : IH2HService
    {
        private readonly IDeletableEntityRepository<Fixture> fixturesRepository;

        public H2HService(IDeletableEntityRepository<Fixture> fixturesRepository)
        {
            this.fixturesRepository = fixturesRepository;
        }

        public async Task<int> HomeTeamWins(int hometeamId, int awayTeamId)
        {

            // taking Home Team in H2H Statistic
            // First taking all wins home team has as a home team
            // second taking all wins home team has as a away team
            // sum both
            int winsAsHomeTeam = await this.fixturesRepository.All()
                .Where(x => x.HomeTeamId == hometeamId && x.AwayTeamId == awayTeamId)
                .Where(x => x.Status == Status.MatchFinished && x.FullTimeExit == FullTimeExit.HomeWin)
                .CountAsync();

            int winsAsAwayTeam = await this.fixturesRepository.All()
                .Where(x => x.HomeTeamId == awayTeamId && x.AwayTeamId == hometeamId)
                .Where(x => x.Status == Status.MatchFinished && x.FullTimeExit == FullTimeExit.AwayWin)
                .CountAsync();

            int totalHomeTeamWins = winsAsHomeTeam + winsAsAwayTeam;

            return totalHomeTeamWins;
        }

        public async Task<int> AwayTeamWins(int hometeamId, int awayTeamId)
        {
            // taking Away Team in H2H Statistic
            // First taking all wins away team has as a home team
            // second taking all wins away team has as a away team
            // sum both

            int winsAsHomeTeam = await this.fixturesRepository.All()
                 .Where(x => x.HomeTeamId == hometeamId && x.AwayTeamId == awayTeamId)
                 .Where(x => x.Status == Status.MatchFinished && x.FullTimeExit == FullTimeExit.AwayWin)
                 .CountAsync();

            int winsAsAwayTeam = await this.fixturesRepository.All()
                .Where(x => x.HomeTeamId == awayTeamId && x.AwayTeamId == hometeamId)
                .Where(x => x.Status == Status.MatchFinished && x.FullTimeExit == FullTimeExit.HomeWin)
                .CountAsync();

            int totalAwayTeamWins = winsAsHomeTeam + winsAsAwayTeam;

            return totalAwayTeamWins;
        }

        public async Task<int> Draws(int hometeamId, int awayTeamId)
        {
            int drawsAsHomeTeam = await this.fixturesRepository.All()
                 .Where(x => x.HomeTeamId == hometeamId && x.AwayTeamId == awayTeamId)
                 .Where(x => x.Status == Status.MatchFinished && x.FullTimeExit == FullTimeExit.Draw)
                 .CountAsync();

            int drawAsAwayteam = await this.fixturesRepository.All()
                .Where(x => x.HomeTeamId == awayTeamId && x.AwayTeamId == hometeamId)
                .Where(x => x.Status == Status.MatchFinished && x.FullTimeExit == FullTimeExit.Draw)
                .CountAsync();

            int totalDraws = drawsAsHomeTeam + drawAsAwayteam;

            return totalDraws;
        }

        public async Task<int> TotalBothTeamsScoredInLast20Games(int teamId)
        {
            // get current team last 20 games
            // check how many in them both had scored
            // btts -> both teams to score

            int btts = await this.fixturesRepository.All()
                .Where(x => x.HomeTeamId == teamId || x.AwayTeamId == teamId)
                .Where(x => x.Status == Status.MatchFinished)
                .OrderByDescending(x => x.KickOff)
                .Take(20)
                .Where(x => x.GoalsHomeTeam > 0 && x.GoalsAwayTeam > 0)
                .CountAsync();

            return btts;

        }

        public async Task<int> TotalOver2point5GoalsInLast20Games(int teamId)
        {
            // get last 20 games for certain team and check
            // how many games are above 2.5 goals
            int totalOver2point5 = await this.fixturesRepository.All()
                .Where(x => x.HomeTeamId == teamId || x.AwayTeamId == teamId)
                .Where(x => x.Status == Status.MatchFinished)
                .OrderByDescending(x => x.KickOff)
                .Take(20)
                .Select(x => new
                {
                    TotalGoals = (double)x.GoalsHomeTeam + (double)x.GoalsAwayTeam,
                })
                .Where(x => x.TotalGoals > 2.5)
                .CountAsync();

            return totalOver2point5;

        }

        public async Task<int> TotalGoalsHomeTeamScored(int hometeamId, int awayTeamId)
        {
            // take all games between those two teams
            // see how many goals home team had scored

            int goalsAsAHomeTeam = await this.fixturesRepository.All()
                .Where(x => x.HomeTeamId == hometeamId && x.AwayTeamId == awayTeamId)
                .Where(x => x.Status == Status.MatchFinished)
                .SumAsync(x => (int)x.GoalsHomeTeam);

            int goalsAsAnAwayTeam = await this.fixturesRepository.All()
                .Where(x => x.HomeTeamId == awayTeamId && x.AwayTeamId == hometeamId)
                .Where(x => x.Status == Status.MatchFinished)
                .SumAsync(x => (int)x.GoalsAwayTeam);

            int totalGoals = goalsAsAHomeTeam + goalsAsAnAwayTeam;

            return totalGoals;
        }

        public async Task<int> TotalGoalsAwayTeamScored(int hometeamId, int awayTeamId)
        {
            // take all games between those two teams
            // see how many goals away team had scored

            int goalsAsAHomeTeam = await this.fixturesRepository.All()
                .Where(x => x.HomeTeamId == awayTeamId && x.AwayTeamId == hometeamId)
                .Where(x => x.Status == Status.MatchFinished)
                .SumAsync(x => (int)x.GoalsHomeTeam);

            int goalsAsAnAwayTeam = await this.fixturesRepository.All()
                .Where(x => x.HomeTeamId == hometeamId && x.AwayTeamId == awayTeamId)
                .Where(x => x.Status == Status.MatchFinished)
                .SumAsync(x => (int)x.GoalsAwayTeam);

            int totalGoals = goalsAsAHomeTeam + goalsAsAnAwayTeam;

            return totalGoals;
        }

        public async Task<int> TotalGoalsScored(int hometeamId, int awayTeamId)
        {
            var goalsHome = await this.fixturesRepository.All()
                .Where(x => x.HomeTeamId == hometeamId && x.AwayTeamId == awayTeamId)
                .SumAsync(x => x.GoalsHomeTeam + x.GoalsAwayTeam);

            var goalsAway = await this.fixturesRepository.All()
               .Where(x => x.HomeTeamId == awayTeamId && x.AwayTeamId == hometeamId)
               .SumAsync(x => x.GoalsHomeTeam + x.GoalsAwayTeam);

            int totalGoals = (int)goalsHome + (int)goalsAway;

            return totalGoals;
        }

        public async Task<int> TotalGamesBetweenTwoTeams(int hometeamId, int awayTeamId)
        {
            var gamesHome = await this.fixturesRepository.All()
                .Where(x => x.HomeTeamId == hometeamId && x.AwayTeamId == awayTeamId)
                .Where(x => x.Status == Status.MatchFinished)
                .CountAsync();

            var gamesAway = await this.fixturesRepository.All()
                .Where(x => x.HomeTeamId == awayTeamId && x.AwayTeamId == hometeamId)
                .Where(x => x.Status == Status.MatchFinished)
                .CountAsync();

            int totalGames = gamesHome + gamesAway;

            return totalGames;
        }

        public async Task<double> HowOftenTeamWinsAsAHomeTeam(int hometeamId)
        {
            int totalGames = await
                 this.fixturesRepository.All().Where(x => x.HomeTeamId == hometeamId)
                .Where(x => x.Status == Status.MatchFinished)
                .CountAsync();

            int totalWins = await
                this.fixturesRepository.All().Where(x => x.HomeTeamId == hometeamId)
                .Where(x => x.Status == Status.MatchFinished)
                .Where(x => x.FullTimeExit == FullTimeExit.HomeWin)
                .CountAsync();

            double totalHomeTeamWins = Math.Truncate(((double)totalWins / (double)totalGames) * 100);

            return totalHomeTeamWins;
        }

        public async Task<double> HowOftenTeamWinsAsAnAwayTeam(int awayteamId)
        {
            int totalGames = await
                 this.fixturesRepository.All().Where(x => x.AwayTeamId == awayteamId)
                  .Where(x => x.Status == Status.MatchFinished)
                .CountAsync();

            int totalWins = await
                this.fixturesRepository.All().Where(x => x.AwayTeamId == awayteamId)
                .Where(x => x.Status == Status.MatchFinished)
                .Where(x => x.FullTimeExit == FullTimeExit.AwayWin)
                .CountAsync();

            double totalHomeTeamWins = Math.Truncate(((double)totalWins / (double)totalGames) * 100);

            return totalHomeTeamWins;
        }

        public async Task<H2HViewModel> PrepareViewModel(int hometeamId, int awayTeamId)
        {
            var viewModel = new H2HViewModel();
            viewModel.HomeTeamWins = await this.HomeTeamWins(hometeamId, awayTeamId);
            viewModel.AwayTeamWins = await this.AwayTeamWins(hometeamId, awayTeamId);
            viewModel.Draws = await this.Draws(hometeamId, awayTeamId);
            viewModel.TotalGoalsScored = await this.TotalGoalsScored(hometeamId, awayTeamId);
            viewModel.TotalGamesBetweenTwoTeams = await this.TotalGamesBetweenTwoTeams(hometeamId, awayTeamId);
            viewModel.TotalGoalsHomeTeamScored = await this.TotalGoalsHomeTeamScored(hometeamId, awayTeamId);
            viewModel.TotalGoalsAwayTeamScored = await this.TotalGoalsAwayTeamScored(hometeamId, awayTeamId);
            viewModel.TotalBttsLast20GamesHomeTeam = await this.TotalBothTeamsScoredInLast20Games(hometeamId);
            viewModel.TotalBttsLast20GamesAwayTeam = await this.TotalBothTeamsScoredInLast20Games(awayTeamId);
            viewModel.TotalOver2point5GoalsInLast20GamesHomeTeam = await this.TotalOver2point5GoalsInLast20Games(hometeamId);
            viewModel.TotalOver2point5GoalsInLast20GamesAwayTeam = await this.TotalOver2point5GoalsInLast20Games(awayTeamId);
            viewModel.HowOftenTeamWinsAsAHomeTeam = await this.HowOftenTeamWinsAsAHomeTeam(hometeamId);
            viewModel.HowOftenTeamWinsAsAnAwayTeam = await this.HowOftenTeamWinsAsAnAwayTeam(awayTeamId);

            return viewModel;
        }

    }
}
