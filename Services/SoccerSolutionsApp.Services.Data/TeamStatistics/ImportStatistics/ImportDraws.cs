namespace SoccerSolutionsApp.Services.Data.Teams.ImportStatistics
{
    using System.Text;

    public class ImportDraws
    {
        public int Home { get; set; }

        public int Away { get; set; }

        public int Total { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Draws as Home Team - {this.Home}");
            sb.AppendLine($"Draws as Away Team - {this.Away}");
            sb.AppendLine($"Draws Total - {this.Total}");

            return sb.ToString().TrimEnd();
        }
    }
}
