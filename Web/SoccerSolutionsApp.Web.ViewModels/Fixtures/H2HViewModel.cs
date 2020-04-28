namespace SoccerSolutionsApp.Web.ViewModels.Fixtures
{
    using System.Collections.Generic;

    public class H2HViewModel
    {
        public int HomeTeamWins { get; set; }

        public int AwayTeamWins { get; set; }

        public int Draws { get; set; }

        public int TotalGoalsScored { get; set; }

        public int TotalGoalsHomeTeamScored { get; set; }

        public int TotalGoalsAwayTeamScored { get; set; }

        public int TotalBothTeamsScoredInLast20Games { get; set; }

        public int TotalOver2point5GoalsInLast20Games { get; set; }

        public IEnumerable<FixtureDetailViewModel> Fixtures { get; set; }
    }
}
