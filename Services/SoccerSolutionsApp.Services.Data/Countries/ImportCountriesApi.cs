namespace SoccerSolutionsApp.Services.Data.Countries
{
    using Newtonsoft.Json;

    public class ImportCountriesApi
    {
        [JsonProperty("api")]
        public ImportCountriesApiModel Api { get; set; }
    }
}
