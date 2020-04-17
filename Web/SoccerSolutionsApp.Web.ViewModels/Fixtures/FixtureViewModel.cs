namespace SoccerSolutionsApp.Web.ViewModels.Fixtures
{
    using System;

    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Data.Models.Enums;
    using SoccerSolutionsApp.Services.Mapping;

    public class FixtureViewModel : IMapFrom<Fixture>
    {
        public DateTime KickOff { get; set; }

        public string HomeTeamName { get; set; }

        public string AwayTeamName { get; set; }

        public string StatusShort { get; set; }

        public Status Status { get; set; }

        public string Fulltime { get; set; }

        public string LeagueCountryName { get; set; }
    }
}
