using Newtonsoft.Json;

namespace SoccerSolutionsApp.Services.Data.Standings
{
    public class ImportStandingsAll
    {
        [JsonProperty("matchsPlayed")]
        public int MatchPlayed { get; set; }

        public int Win { get; set; }

        public int Draw { get; set; }

        public int Lose { get; set; }

        [JsonProperty("goalsFor")]
        public int GoalsFor { get; set; }

        [JsonProperty("goalsAgainst")]
        public int GoalsAgainst { get; set; }


    }
}
