using System.Text;

namespace SoccerSolutionsApp.Services.Data.Fixtures
{
    public class ImportScoreApiModel
    {
        public string HalfTime { get; set; }

        public string FullTime { get; set; }

        public string Extratime { get; set; }

        public string Penalty { get; set; }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            string halftime = this.HalfTime != null ? this.HalfTime : "null";
            sb.AppendLine($"halftime - {halftime}");

            string fulltime = this.FullTime != null ? this.FullTime : "null";
            sb.AppendLine($"halftime - {fulltime}");

            string extratime = this.Extratime != null ? this.Extratime : "null";
            sb.AppendLine($"halftime - {extratime}");


            string penalty = this.Penalty != null ? this.Penalty : "null";
            sb.AppendLine($"halftime - {penalty}");

            return sb.ToString().TrimEnd();

        }
    }
}
