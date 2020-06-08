namespace SoccerSolutionsApp.Services.Data.Fixtures
{
    using Newtonsoft.Json;
    using System;
    using System.Text;

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

        public ImportHomeTeamModel HomeTeam { get; set; }

        public ImportAwayTeamModel AwayTeam { get; set; }

        public int? GoalsHomeTeam { get; set; }

        public int? GoalsAwayTeam { get; set; }

        public ImportScoreApiModel Score { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"fixture id = {this.FixtureId}");
            sb.AppendLine($"league id = {this.LeagueId}");
            sb.AppendLine($"event date = {this.EventDate}");

            string round = this.Round != null ? this.Round : "null";
            sb.AppendLine($"round: {round}");

            string status = this.Status != null ? this.Status : "null";
            sb.AppendLine($"status: {status}");

            string statusShort = this.StatusShort != null ? this.Status : "null";
            sb.AppendLine($"status short: {statusShort}");

            sb.AppendLine($"Elapsed: {this.Elapsed}");

            string venue = this.Venue != null ? this.Venue : "null";
            sb.AppendLine($"venue: {venue}");

            string referee = this.Referee != null ? this.Referee : "null";
            sb.AppendLine($"referre: {referee}");

            string homeTeamId = this.HomeTeam.TeamId != null ? this.HomeTeam.TeamId.ToString() : "null";
            sb.AppendLine($"home team id: {homeTeamId}");

            string awayTeamId = this.AwayTeam.TeamId != null ? this.AwayTeam.TeamId.ToString() : "null";
            sb.AppendLine($"away team id: {awayTeamId}");

            string goalsHomeTeam = this.GoalsHomeTeam != null ? this.GoalsHomeTeam.ToString() : "null";
            sb.AppendLine($"goals home team: {goalsHomeTeam}");

            string goalsAwayTeam = this.GoalsHomeTeam != null ? this.GoalsHomeTeam.ToString() : "null";
            sb.AppendLine($"goals away team: {goalsAwayTeam}");

            sb.AppendLine(this.Score.ToString());

            return sb.ToString().TrimEnd();
        }
    }
}
