namespace SoccerSolutionsApp.Web.ViewModels.Main
{
    using System;

    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Data.Models.Enums;
    using SoccerSolutionsApp.Services.Mapping;

    public class FixturesListingViewModel : IMapFrom<Fixture>
    {
        public DateTime KickOff { get; set; }

        public Status Status { get; set; }

        public string StatusShort { get; set; }

        public string FullTime { get; set; }

        public string HomeTeamName { get; set; }

        public string AwayTeamName { get; set; }

        public string StatusToShow { get; set; }
    }
}
