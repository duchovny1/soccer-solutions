namespace SoccerSolutionsApp.Services.Data.Fixtures
{
    using Newtonsoft.Json;
    using System;

    public class ImportFixturesApiModel
    {
        [JsonProperty("fixture_id")]
        public int FixtureId { get; set; }

        [JsonProperty("league_id")]
        public int LeagueId { get; set; }

        [JsonProperty("event_date")]
        public DateTime EventDate { get; set; }

        public string Round { get; set; }

        public string Status { get; set; }

        public string StatusShort { get; set; }

        public int Elapsed { get; set; }

        public string Venue { get; set; }

        public string Referee { get; set; }

        [JsonProperty("homeTeam.team_id")]
        public int HomeTeamId { get; set; }

        [JsonProperty("awayTeam.team_id")]
        public int AwayTeamId { get; set; }

        public int GoalsHomeTeam { get; set; }

        public int GoalsAwayTeam { get; set; }

        public ImportScoreApiModel Score { get; set; }


    }
}
