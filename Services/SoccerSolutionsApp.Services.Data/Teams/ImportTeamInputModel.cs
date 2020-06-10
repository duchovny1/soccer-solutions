namespace SoccerSolutionsApp.Services.Data.Teams
{
    using Newtonsoft.Json;
    using System.Text;

    public class ImportTeamInputModel
    {
        [JsonProperty("team_id")]

        public int TeamId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Logo { get; set; }

        [JsonProperty("is_national")]
        public bool IsNational { get; set; }

        public int? Founded { get; set; }

        [JsonProperty("venue_name")]
        public string VenueName { get; set; }

        [JsonProperty("venue_capacity")]
        public int? VenueCapacity { get; set; }

        public string Country { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Team id - {this.TeamId}");
            sb.AppendLine($"Name - {this.Name}");
            sb.AppendLine($"Code - {this.Code}");
            sb.AppendLine($"IsNational - {this.IsNational}");
            sb.AppendLine($"Founded - {this.Founded}");
            sb.AppendLine($"Venue name - {this.VenueName}");
            sb.AppendLine($"Venue capacity - {this.VenueCapacity}");
            sb.AppendLine($"Country - {this.Country}");

            return sb.ToString().TrimEnd();
        }

    }
}
