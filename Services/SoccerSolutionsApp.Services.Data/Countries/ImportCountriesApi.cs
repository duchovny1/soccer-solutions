namespace SoccerSolutionsApp.Services.Data.Countries
{
    using Newtonsoft.Json;

    public class ImportCountriesApi
    {
        [JsonProperty("results")]
        public int Results { get; set; }


        [JsonProperty("countries")]
        public ImportCountriesServiceModel[] Countries { get; set; }
    }
}
