namespace SoccerSolutionsApp.Web.ViewModels.Fixtures
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Services.Mapping;

    public class FixtureDetailViewModel : IMapFrom<Fixture>
    {
        public int Id { get; set; }

        public DateTime KickOff { get; set; }

        public string KickOffForView => this.KickOff.ToString("dd") + " "
                                       + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(KickOff.Month)
                                       + " " + this.KickOff.Year.ToString();

        public int HomeTeamId { get; set; }

        public string Round { get; set; }

        public string RoundShort => this.Round.Substring(this.Round.Length - 2);

        public int AwayTeamId { get; set; }

        public string HomeTeamLogo { get; set; }

        public string AwayTeamLogo { get; set; }

        public string HomeTeamName { get; set; }

        public string AwayTeamName { get; set; }

        public string Status { get; set; }

        public string Score { get; set; }

        public string HalfTime { get; set; }

        public string FullTime { get; set; }

        public string Venue { get; set; }

        public string LeagueName { get; set; }

        public IEnumerable<PastFixturesViewModel> HomeTeamPastMatches { get; set; }

        public IEnumerable<NextFixturesViewModel> HomeTeamNextMatches { get; set; }

        public IEnumerable<PastFixturesViewModel> AwayTeamPastMatches { get; set; }

        public IEnumerable<NextFixturesViewModel> AwayTeamNextMatches { get; set; }


    }
}
