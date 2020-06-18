namespace SoccerSolutionsApp.Services.Data.Teams.ImportStatistics
{
    using System.Text;

    public class ImportGoalsAgainstDecimal
    {
        public string Home { get; set; }

        public string Away { get; set; }

        public string Total { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            string home = this.Home != null ? this.Home : "null";
            string away = this.Away != null ? this.Away : "null";
            string total = this.Total != null ? this.Total : "null";

            sb.AppendLine($"Goals Against AVG as Home Team - {home}");
            sb.AppendLine($"Goals Against AVG as Away Team - {away}");
            sb.AppendLine($"Goals Against AVG Total - {total}");

            return sb.ToString().TrimEnd();
        }
    }
}