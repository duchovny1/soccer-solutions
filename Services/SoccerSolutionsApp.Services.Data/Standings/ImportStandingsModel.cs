namespace SoccerSolutionsApp.Services.Data.Standings
{
    using Newtonsoft.Json;

    public class ImportStandingsModel
    {
        public int Rank { get; set; }

        [JsonProperty("team_id")]
        public int TeamId { get; set; }

        public string Group { get; set; }

        public string Forme { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }

        public ImportStandingsAll All { get; set; }

        [JsonProperty("goalsDiff")]
        public int GoalsDiff { get; set; }

        public int Points { get; set; }
    }
}