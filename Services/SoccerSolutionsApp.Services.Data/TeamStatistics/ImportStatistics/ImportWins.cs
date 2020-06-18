namespace SoccerSolutionsApp.Services.Data.Teams.ImportStatistics
{
    using System.Text;

    public class ImportWins
    {
        public int Home { get; set; }

        public int Away { get; set; }

        public int Total { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Wins as Home Team - {this.Home}");
            sb.AppendLine($"Wins as Away Team - {this.Away}");
            sb.AppendLine($"Wins Total - {this.Total}");

            return sb.ToString().TrimEnd();
        }
    }
}
