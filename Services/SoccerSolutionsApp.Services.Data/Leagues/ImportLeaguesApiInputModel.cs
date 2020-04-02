using Newtonsoft.Json;

namespace SoccerSolutionsApp.Services.Data.Leagues
{
    public class ImportLeaguesApiInputModel
    {
        //        "league_id":2
        //"name":"Premier League"
        //"type":"League"
        //"country":"England"
        //"country_code":"GB"
        //"season":2018
        //"season_start":"2018-08-10"
        //"season_end":"2019-05-12"
        //"logo":"https://media.api-football.com/leagues/2.png"
        //"flag":"https://media.api-football.com/flags/gb.svg"



        [JsonProperty("league_id")]
        public int LeagueId { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Country { get; set; }

        public string Season { get; set; }

        [JsonProperty("season_start")]
        public string SeasonStart { get; set; }

        [JsonProperty("season_end")]
        public string SeasonEnd { get; set; }

        public string Logo { get; set; }
    }
}
