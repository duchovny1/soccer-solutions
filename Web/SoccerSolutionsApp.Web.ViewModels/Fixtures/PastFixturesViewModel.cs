namespace SoccerSolutionsApp.Web.ViewModels.Fixtures
{
    using System;

    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Services.Mapping;

    public class PastFixturesViewModel : IMapFrom<Fixture>
    {
        public DateTime Date { get; set; }

        public int HomeTeamId { get; set; }

        public string HomeTeamName { get; set; }

        public int AwayTeamId { get; set; }

        public string AwayTeamName { get; set; }

        public string LeagueShort { get; set; }

    }
}
