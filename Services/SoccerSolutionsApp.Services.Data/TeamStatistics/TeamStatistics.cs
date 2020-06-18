namespace SoccerSolutionsApp.Services.Data.TeamStatistics
{
    using SoccerSolutionsApp.Data.Common.Repositories;
    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Services.Data.Teams.ImportStatistics;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class TeamStatisticsService : ITeamStatisticsService
    {
        private readonly IDeletableEntityRepository<Statistics> teamStatisticsRepository;

        public TeamStatisticsService(IDeletableEntityRepository<Statistics> teamStatisticsRepository)
        {
            this.teamStatisticsRepository = teamStatisticsRepository;
        }

        public string FilePath { get; } = Path.Combine(Environment.CurrentDirectory + "\\" + "Team Statistics.txt");

        public bool AddStatistics(ImportStatisticsApi inputModel, int teamId, int leagueId)
        {
            bool isExists = this.teamStatisticsRepository.AllWithDeleted().Any(x => x.TeamId == teamId && x.LeagueId == leagueId);

            if (isExists)
            {
                return false;
            }

            try
            {
                var statistics = new Statistics
                {
                    TeamId = teamId,
                    LeagueId = leagueId,
                    MatchPlayedAsHomeTeam = inputModel.Statistics.Matchs.MatchsPlayed.Home,
                    MatchPlayedAsAwayTeam = inputModel.Statistics.Matchs.MatchsPlayed.Away,
                    MatchPlayedTotal = inputModel.Statistics.Matchs.MatchsPlayed.Total,
                    MatchesWinsAsHome = inputModel.Statistics.Matchs.Wins.Home,
                    MatchesWinsAsAway = inputModel.Statistics.Matchs.Wins.Away,
                    MatchesWinsTotal = inputModel.Statistics.Matchs.Wins.Total,
                    MatchesDrawsAsHome = inputModel.Statistics.Matchs.Draws.Home,
                    MatchesDrawsAsAway = inputModel.Statistics.Matchs.Draws.Away,
                    MatchesDrawsTotal = inputModel.Statistics.Matchs.Draws.Total,
                    MatchesLosesAsHome = inputModel.Statistics.Matchs.Loses.Home,
                    MatchesLosesAsAway = inputModel.Statistics.Matchs.Loses.Away,
                    MatchesLosesTotal = inputModel.Statistics.Matchs.Loses.Total,
                    GoalsForAsHome = inputModel.Statistics.Goals.GoalsFor.Home,
                    GoalsForAsAway = inputModel.Statistics.Goals.GoalsFor.Away,
                    GoalsForTotal = inputModel.Statistics.Goals.GoalsFor.Total,
                    GoalsAgainstAsHome = inputModel.Statistics.Goals.GoalsAgainst.Home,
                    GoalsAgainstAsAway = inputModel.Statistics.Goals.GoalsAgainst.Away,
                    GoalsAgainstTotal = inputModel.Statistics.Goals.GoalsAgainst.Total,

                    // Avg is average / for example arsenal scores average 1.7 goals per home/away game

                    GoalsForAvgAsHome = double.Parse(inputModel.Statistics.GoalsAvg.GoalsFor.Home),
                    GoalsForAvgAsAway = double.Parse(inputModel.Statistics.GoalsAvg.GoalsFor.Away),
                    GoalsForAvgTotal = double.Parse(inputModel.Statistics.GoalsAvg.GoalsFor.Total),
                    GoalsAgainstAvgAsHome = double.Parse(inputModel.Statistics.GoalsAvg.GoalsAgainst.Home),
                    GoalsAgainstAvgAsAway = double.Parse(inputModel.Statistics.GoalsAvg.GoalsAgainst.Away),
                    GoalsAgainstAvgTotal = double.Parse(inputModel.Statistics.GoalsAvg.GoalsAgainst.Total),
                };

                this.teamStatisticsRepository.Add(statistics);
                this.teamStatisticsRepository.SaveChanges();

                return true;
            }

            // catching all еxceptions in the hierarchy in order to avoid code repetition in every catch block
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                using StreamWriter sw = new StreamWriter(this.FilePath, true);

                sb.AppendLine($"An exception occured at {DateTime.UtcNow}");
                sb.AppendLine($"There is some problem with deserializating Teams Statistics json for team with id {teamId} from league with id {leagueId}");
                sb.AppendLine($"Original exception message: ");
                sb.AppendLine(ex.Message);

                if (ex.InnerException != null)
                {
                    sb.AppendLine(ex.InnerException.ToString());
                }

                sb.AppendLine("Statistics Object: ");

                sb.AppendLine(inputModel.Statistics.Matchs.MatchsPlayed.ToString());
                sb.AppendLine(inputModel.Statistics.Matchs.Wins.ToString());
                sb.AppendLine(inputModel.Statistics.Matchs.Draws.ToString());
                sb.AppendLine(inputModel.Statistics.Matchs.Loses.ToString());
                sb.AppendLine(inputModel.Statistics.Goals.GoalsFor.ToString());
                sb.AppendLine(inputModel.Statistics.Goals.GoalsAgainst.ToString());
                sb.AppendLine(inputModel.Statistics.GoalsAvg.GoalsFor.ToString());
                sb.AppendLine(inputModel.Statistics.GoalsAvg.GoalsAgainst.ToString());

                sb.AppendLine("End of exception here.");
                sb.AppendLine("--------------");
                sb.AppendLine("---------------");
                sw.Write(sb.ToString());
                sw.Close();

                return false;
            }
        }

        public bool Update(ImportStatisticsApi inputModel, int teamId, int leagueId)
        {
            throw new NotImplementedException();
        }
    }
}
