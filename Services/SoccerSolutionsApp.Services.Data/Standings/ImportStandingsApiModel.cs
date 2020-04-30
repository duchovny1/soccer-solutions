namespace SoccerSolutionsApp.Services.Data.Standings
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class ImportStandingsApiModel
    {
        public int Results { get; set; }


        [JsonProperty("0")]
        public Dictionary<int, Dictionary<int, ImportStandingsModel[]>> Standings { get; set; }
    }
}