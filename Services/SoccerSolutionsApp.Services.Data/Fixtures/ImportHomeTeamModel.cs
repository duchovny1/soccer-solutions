namespace SoccerSolutionsApp.Services.Data.Fixtures
{
    using Newtonsoft.Json;

    public class ImportHomeTeamModel
    {
        [JsonProperty("team_id")]
        public int? TeamId { get; set; }
    }
}
