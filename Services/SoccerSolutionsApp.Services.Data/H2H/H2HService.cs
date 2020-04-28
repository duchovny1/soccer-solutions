namespace SoccerSolutionsApp.Services.Data.H2H
{
    using Microsoft.EntityFrameworkCore;
    using SoccerSolutionsApp.Data.Common.Repositories;
    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Data.Models.Enums;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

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

            int totalHomeTeamWins = winsAsHomeTeam + winsAsHomeTeam;

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

            int totalAwayTeamWins = winsAsHomeTeam + winsAsHomeTeam;

            return totalAwayTeamWins;
        }

        public async Task<int> Draws(int hometeamId, int awayTeamId)
        {
            int winsAsHomeTeam = await this.fixturesRepository.All()
                 .Where(x => x.HomeTeamId == hometeamId && x.AwayTeamId == awayTeamId)
                 .Where(x => x.Status == Status.MatchFinished && x.FullTimeExit == FullTimeExit.Draw)
                 .CountAsync();

            int winsAsAwayTeam = await this.fixturesRepository.All()
                .Where(x => x.HomeTeamId == awayTeamId && x.AwayTeamId == hometeamId)
                .Where(x => x.Status == Status.MatchFinished && x.FullTimeExit == FullTimeExit.Draw)
                .CountAsync();

            int totalDraws = winsAsHomeTeam + winsAsHomeTeam;

            return totalDraws;
        }


        public async Task<int> TotalBothTeamsScoredInLast20Games(int teamId)
        {
            // get current team last 20 games
            // check how many in them both had scored
            // btts -> both teams to score 
            int btts = await this.fixturesRepository.All()
                .Where(x => x.HomeTeamId == teamId && x.AwayTeamId == teamId)
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

        public Task<int> TotalGoalsAwayTeamScored(int hometeamId, int awayTeamId)
        {
            throw new NotImplementedException();
        }


        public Task<int> TotalGoalsScored(int hometeamId, int awayTeamId)
        {
            throw new NotImplementedException();
        }

       
    }
}
