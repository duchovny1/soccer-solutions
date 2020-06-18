namespace SoccerSolutionsApp.Services.Data.Teams.ImportStatistics
{
    using System.Text;

    public class ImportGoalsFor
    {
        public int Home { get; set; }

        public int Away { get; set; }

        public int Total { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Goals For as Home Team - {this.Home}");
            sb.AppendLine($"Goals For as Away Team - {this.Away}");
            sb.AppendLine($"Goals For Total - {this.Total}");

            return sb.ToString().TrimEnd();
        }
    }
}
