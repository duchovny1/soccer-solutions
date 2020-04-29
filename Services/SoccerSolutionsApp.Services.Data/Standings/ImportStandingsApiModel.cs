namespace SoccerSolutionsApp.Services.Data.Standings
{
    using System.Collections.Generic;

    public class ImportStandingsApiModel
    {
        public int Results { get; set; }

        public IEnumerable<ImportStandingsModel> Standings { get; set; }
    }
}