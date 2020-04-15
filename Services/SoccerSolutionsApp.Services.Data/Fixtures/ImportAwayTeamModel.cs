namespace SoccerSolutionsApp.Services.Data.Fixtures
{
    using Newtonsoft.Json;

    public class ImportAwayTeamModel
    {
        [JsonProperty("team_id")]
        public int? TeamId { get; set; }
    }
}
