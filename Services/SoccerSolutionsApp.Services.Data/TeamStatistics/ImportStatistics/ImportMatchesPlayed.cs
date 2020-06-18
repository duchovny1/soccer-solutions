namespace SoccerSolutionsApp.Services.Data.Teams.ImportStatistics
{
    using System.Text;

    public class ImportMatchesPlayed
    {
        public int Home { get; set; }

        public int Away { get; set; }

        public int Total { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Matches Played as Home Team - {this.Home}");
            sb.AppendLine($"Matches Played as Away Team - {this.Away}");
            sb.AppendLine($"Matches Played Total - {this.Total}");

            return sb.ToString().TrimEnd();
        }
    }
}