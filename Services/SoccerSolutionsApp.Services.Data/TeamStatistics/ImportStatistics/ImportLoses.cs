namespace SoccerSolutionsApp.Services.Data.Teams.ImportStatistics
{
    using System.Text;

    public class ImportLoses
    {
        public int Home { get; set; }

        public int Away { get; set; }

        public int Total { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Loses as Home Team - {this.Home}");
            sb.AppendLine($"Loses as Away Team - {this.Away}");
            sb.AppendLine($"Loses Total - {this.Total}");

            return sb.ToString().TrimEnd();
        }

    }
}
