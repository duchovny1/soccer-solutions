namespace SoccerSolutionsApp.Services.Data.Tests
{
    using System;
    using System.Reflection;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SoccerSolutionsApp.Data;
    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Data.Repositories;
    using SoccerSolutionsApp.Services.Data.Leagues;
    using SoccerSolutionsApp.Services.Mapping;
    using Xunit;

    public class LeaguesServiceTests
    {
        private readonly ILeaguesService leaguesService;
        private EfDeletableEntityRepository<League> leaguesRepository;
        private EfDeletableEntityRepository<Country> countryRepository;
        private EfDeletableEntityRepository<TeamLeagues> teamLeagueRepository;
        private EfDeletableEntityRepository<Season> seasonRepository;
        private EfDeletableEntityRepository<Fixture> fixturesRepository;

        public LeaguesServiceTests()
        {
            this.InitializeDatabaseAndRepositories();
            this.InitializeMapper();

            this.leaguesService = new LeaguesService(
                this.leaguesRepository,
                this.countryRepository,
                this.seasonRepository,
                this.fixturesRepository,
                this.teamLeagueRepository);
        }


        [Fact]
        public async Task TestAddingALeague()
        {
            this.SeedSeasonsData();
            this.SeedCountriesData();

            var inputModel = new ImportLeaguesApi()
            {
                Api = new ImportLeaguesApiModel()
                {
                    Results = 1,
                    Leagues = new ImportLeaguesApiInputModel[]
                    {
                        new ImportLeaguesApiInputModel()
                        {
                              LeagueId = 2,
                              Name = "Premier League",
                              Type = "League",
                              Country = "England",
                              Season = "2019",
                              SeasonStart = "2018-08-10",
                              SeasonEnd = "2019-05-12",
                              Logo = "https://media.api-football.com/leagues/2.png",
                        },
                    },
                },
            };


            int createdLeagues = this.leaguesService.Create(inputModel);
            var totalLeagues = await this.leaguesRepository.All().CountAsync();

            Assert.Equal(1, createdLeagues);


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
        }


        private void SeedSeasonsData()
        {
            var season = SeedDataTests.GetSeasonData();
            this.seasonRepository.Add(season);
            this.seasonRepository.SaveChanges();
        }

        private void SeedCountriesData()
        {
            var country = SeedDataTests.GetCountriesData();
            
            this.countryRepository.Add(country);

            this.countryRepository.SaveChanges();
        }

        private void InitializeDatabaseAndRepositories()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);

            this.leaguesRepository = new EfDeletableEntityRepository<League>(context);
            this.seasonRepository = new EfDeletableEntityRepository<Season>(context);
            this.countryRepository = new EfDeletableEntityRepository<Country>(context);
            this.fixturesRepository = new EfDeletableEntityRepository<Fixture>(context);
            this.teamLeagueRepository = new EfDeletableEntityRepository<TeamLeagues>(context);
        }

        private void InitializeMapper() => AutoMapperConfig.
           RegisterMappings(Assembly.Load("SoccerSolutionsApp.Web.ViewModels"));


    }
}
