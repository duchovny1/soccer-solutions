namespace SoccerSolutionsApp.Services.Data.Seasons
{ 
    using Newtonsoft.Json;

    public class ImportSeasonsApi
    {
        [JsonProperty("api")]
        public ImportSeasonsApiModel Api { get; set; }
    }
}
