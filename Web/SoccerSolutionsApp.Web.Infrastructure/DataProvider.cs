namespace SoccerSolutionsApp.Web.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public static class DataProvider
    {
        // api host
        private const string Host = "x-rapidapi-host";

        // api host value
        private const string HostValue = "api-football-v1.p.rapidapi.com";

        // api header
        private const string Key = "x-rapidapi-key";

        // api header value
        private const string KeyValue = "4647dae471mshba2a7fa64dde9abp117a98jsnf184cf64a1da";

        // api get countries url
        private const string CountriesUrl = "https://api-football-v1.p.rapidapi.com/v2/countries";

        // api get seasons url
        private const string SeasonsUrl = "https://api-football-v1.p.rapidapi.com/v2/seasons";

        // api get leagues by country url
        private const string LeaguesByCountryUrl = "https://api-football-v1.p.rapidapi.com/v2/leagues/country/{countryName}/{season}";

        // api get teams by league id url
        private const string TeamsByIdUrl = "https://api-football-v1.p.rapidapi.com/v2/teams/league/{leagueId}";

        // api get fixtures by league id url
        private const string FixturesByLeagueIdUrl = "https://api-football-v1.p.rapidapi.com/v2/fixtures/league/{leagueId}";

        // api get next fixture by league id url. there is a number of fixtures which is optional
        private const string NextFixturesByLeagueIdUrl = "https://api-football-v1.p.rapidapi.com/v2/fixtures/league/{leagueId}/next/{number}";

        // api get head to head by two teams ids url
        private const string HeadToHeadUrl = "https://api-football-v1.p.rapidapi.com/v2/fixtures/h2h/{team1id}/{team2id}";

        // api get standings by league id url
        private const string StandingsUrl = "https://api-football-v1.p.rapidapi.com/v2/leagueTable/{leagueId}";

        public static string ApiHost { get; } = Host;

        public static string ApiHostValue { get; } = HostValue;

        public static string ApiKey { get; } = Key;

        public static string ApiKeyValue { get; } = KeyValue;

        public static string GetCountriesUrl { get; } = CountriesUrl;

        public static string GetSeasonsUrl { get; } = SeasonsUrl;

        public static string GetLeaguesByCountryUrl { get; } = LeaguesByCountryUrl;

        public static string GetTeamsByIdUrl { get; } = TeamsByIdUrl;

        public static string GetFixturesByIdUrl { get; } = FixturesByLeagueIdUrl;

        public static string GetNexturesByLeagueIdUrl { get; } = NextFixturesByLeagueIdUrl;

        public static string GetHeadToHeadUrl { get; } = HeadToHeadUrl;

        public static string GetStandingsUrl { get; } = StandingsUrl;
    }
}
