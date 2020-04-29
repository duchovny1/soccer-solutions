namespace SoccerSolutionsApp.Web.ViewModels.H2H
{
    using System.Collections.Generic;

    using SoccerSolutionsApp.Web.ViewModels.Fixtures;

    public class H2HViewModel
    {
        public int HomeTeamWins { get; set; }

        public int AwayTeamWins { get; set; }

        public int Draws { get; set; }

        public int TotalGoalsScored { get; set; }

        public int TotalGamesBetweenTwoTeams { get; set; }

        public int TotalGoalsHomeTeamScored { get; set; }

        public int TotalGoalsAwayTeamScored { get; set; }

        // bttts => both teams to scroe
        public int TotalBttsLast20GamesHomeTeam { get; set; }

        public int TotalBttsLast20GamesAwayTeam { get; set; }

        public int TotalOver2point5GoalsInLast20GamesHomeTeam { get; set; }

        public int TotalOver2point5GoalsInLast20GamesAwayTeam { get; set; }

        public double HowOftenTeamWinsAsAHomeTeam { get; set; }

        public double HowOftenTeamWinsAsAnAwayTeam { get; set; }

        public H2HTeamsInfoViewModel TeamsDetails { get; set; }

        public IEnumerable<PastFixturesViewModel> Fixtures { get; set; }
    }
}
