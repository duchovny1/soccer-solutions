namespace SoccerSolutionsApp.Services.Data.Teams
{
    using Newtonsoft.Json;

    public class ImportTeamInputModel
    {
        [JsonProperty("team_id")]

        public int TeamId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Logo { get; set; }

        [JsonProperty("is_national")]
        public bool IsNational { get; set; }

        public int Founded { get; set; }

        [JsonProperty("venue_name")]
        public string VenueName { get; set; }

        [JsonProperty("venue_capacity")]
        public int VenueCapacity { get; set; }

        public string Country { get; set; }

    }
}
