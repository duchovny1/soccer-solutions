namespace SoccerSolutionsApp.Web.ViewModels.Fixtures
{
    using System.Collections.Generic;

    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Services.Mapping;

    public class FixtureDetailViewModel : IMapFrom<Fixture>
    {

        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public string HomeTeamLogo { get; set; }

        public string AwayTeamLogo { get; set; }

        public string HomeTeamName { get; set; }

        public string AwayTeamName { get; set; }

        public string Status { get; set; }

        public string Score { get; set; }

        public string HalfTime { get; set; }

        public string FullTime { get; set; }

        public string LeagueName { get; set; }

        public IEnumerable<PastFixturesViewModel> HomeTeamMatches { get; set; }

        public IEnumerable<PastFixturesViewModel> AwayTeamMatches { get; set; }

    }
}
