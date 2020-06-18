namespace SoccerSolutionsApp.Services.Data.Teams.ImportStatistics
{
    using System.Text;

    public class ImportGoalsAgainst
    {
        public int Home { get; set; }

        public int Away { get; set; }

        public int Total { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Goals Against as Home Team - {this.Home}");
            sb.AppendLine($"Goals Against as Away Team - {this.Away}");
            sb.AppendLine($"Goals Against Total - {this.Total}");

            return sb.ToString().TrimEnd();
        }
    }
}
