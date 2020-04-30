namespace SoccerSolutionsApp.Web.ViewModels.Fixtures
{
    using System;

    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Services.Mapping;

    public class PastFixturesViewModel : IMapFrom<Fixture>
    {
        public DateTime KickOff { get; set; }

        public int HomeTeamId { get; set; }

        public string HomeTeamName { get; set; }

        public int AwayTeamId { get; set; }

        public string AwayTeamName { get; set; }

        public string LeagueLeagueShort { get; set; }

        public string FullTime { get; set; }

        public string FullTimeForView => this.FullTime == null ? "PSTP" : string.Join(" ", this.FullTime.ToCharArray());

        public string StatusShort { get; set; }

        public string DayShort => this.KickOff.DayOfWeek.ToString().Substring(0, 3).ToUpper();
    }
}
