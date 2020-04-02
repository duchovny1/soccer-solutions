namespace SoccerSolutionsApp.Services.Data.Countries
{
    using Newtonsoft.Json;

    public class ImportApi
    {
        [JsonProperty("api")]
        public ImportCountriesApi Api { get; set; }
    }
}
