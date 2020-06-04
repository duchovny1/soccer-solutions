namespace SoccerSolutionsApp.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SoccerSolutionsApp.Data;
    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Data.Repositories;
    using SoccerSolutionsApp.Services.Data.Leagues;
    using SoccerSolutionsApp.Services.Mapping;
    using SoccerSolutionsApp.Web.ViewModels.Leagues;
    using Xunit;

    public class LeaguesServiceTests
    {
        private readonly ILeaguesService leaguesService;
        private EfDeletableEntityRepository<League> leaguesRepository;
        private EfDeletableEntityRepository<Country> countryRepository;
        private EfDeletableEntityRepository<TeamLeagues> teamLeagueRepository;
        private EfDeletableEntityRepository<Team> teamRepository;
        private EfDeletableEntityRepository<Season> seasonRepository;
        private EfDeletableEntityRepository<Fixture> fixturesRepository;

        public LeaguesServiceTests()
        {
            this.InitializeDatabaseAndRepositories();
            this.InitializeApiModel();
            this.InitializeMapper();

            this.leaguesService = new LeaguesService(
                this.leaguesRepository,
                this.countryRepository,
                this.seasonRepository,
                this.fixturesRepository,
                this.teamLeagueRepository);
        }

        public ImportLeaguesApi ApiModel { get; private set; }

        [Fact]
        public async Task TestAddingALeague()
        {
            this.SeedSeasonsData();
            this.SeedCountriesData();

            int createdLeagues = this.leaguesService.Create(this.ApiModel);
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

        [Fact]
        public async Task ShouldNotCreateALeagueWithIncorectSeason()
        {
            this.SeedSeasonsData();
            this.SeedCountriesData();
            this.ApiModel.Api.Leagues = new ImportLeaguesApiInputModel[]
                    {
                        new ImportLeaguesApiInputModel()
                        {
                              LeagueId = 2,
                              Name = "Premier League",
                              Type = "League",
                              Country = "England",
                              Season = "2017",
                              SeasonStart = "2018-08-10",
                              SeasonEnd = "2019-05-12",
                              Logo = "https://media.api-football.com/leagues/2.png",
                        },
                    };

            this.leaguesService.Create(this.ApiModel);
            var seasonsCount = await this.seasonRepository.All().CountAsync();

            Assert.Equal(2, seasonsCount);
            Assert.Empty(this.leaguesRepository.All());
        }

        [Fact]
        public async Task ShouldNotCreateALeagueThatsAlreadyCreated()
        {
            this.SeedSeasonsData();
            this.SeedCountriesData();

            this.leaguesService.Create(this.ApiModel);
            int totalLeagues = await this.leaguesRepository.All().CountAsync();
            Assert.Equal(1, totalLeagues);

            this.leaguesService.Create(this.ApiModel);
            totalLeagues = await this.leaguesRepository.All().CountAsync();
            Assert.Equal(1, this.leaguesRepository.All().Count());
        }


        [Fact]
        public async Task ShouldNotAddLeagueIfCountryIsNull()
        {
            this.SeedSeasonsData();
            this.leaguesService.Create(this.ApiModel);
            int totalLeagues = await this.leaguesRepository.All().CountAsync();
            Assert.Equal(0, totalLeagues);
        }

        [Fact]
        public async Task ShouldNotAddLeagueIfSeasonIsNull()
        {
            this.SeedCountriesData();
            this.leaguesService.Create(this.ApiModel);
            int totalLeagues = await this.leaguesRepository.All().CountAsync();
            Assert.Equal(0, totalLeagues);
        }

        [Fact]
        public void GetAllShouldReturnRightLeagues()
        {
            this.SeedSeasonsData();
            this.SeedCountriesData();
            var leagueNames = this.SeedLeaguesData();

            var leagues = this.leaguesService.GetAll<LeaguesListingViewModel>();

            Assert.NotEmpty(leagues);
            Assert.Collection(
                leagues,
                leagueItem => Assert.Equal(leagueNames[1], leagueItem.Name),
                leagueItem => Assert.Equal(leagueNames[0], leagueItem.Name));
                // adding in database Premier League and Championship
                // but taking them out in reverse order - Championship, Premier League
        }

        [Fact]
        public void GetLeaguesIdShouldReturnRightIds()
        {
            this.SeedSeasonsData();
            this.SeedCountriesData();
            this.SeedLeaguesData();

            var ids = this.leaguesService.GetAllLeaguesId();

            Assert.Contains(1, ids);
            Assert.Contains(2, ids);
        }

        [Fact]
        public async Task GetLeaguesByCountryIdShouldReturnRightLeagues()
        {
            this.SeedSeasonsData();
            this.SeedCountriesData();
            var names = this.SeedLeaguesData();

            var leagues = await this.leaguesService.GetLeaguesByCountryId(1);

            Assert.NotNull(leagues);
            Assert.Contains(leagues, x => x.Name == names[0]);
            Assert.Contains(leagues, x => x.Name == names[1]);
        }


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetLeaguesForTeamsShouldReturnRightLeagues(int teamId)
        {
            this.SeedSeasonsData();
            this.SeedCountriesData();
            this.SeedLeaguesData();
            this.SeedTeamsData();
            this.SeedTeamsLeaguesData();

            var leagues = await this.leaguesService.GetLeaguesForTeam(teamId);

            Assert.Contains(leagues, x => x.Id == 1);
        }


        private List<string> SeedLeaguesData()
        {
            var leagues = SeedDataTests.GetLeaguesData();

            List<string> names = new List<string>();

            foreach (var league in leagues)
            {
                this.leaguesRepository.Add(league);
                names.Add(league.Name);
            }

            this.leaguesRepository.SaveChanges();

            return names;
        }

        private void SeedSeasonsData()
        {
            var seasons = SeedDataTests.GetSeasonData();

            foreach (var season in seasons)
            {
                this.seasonRepository.Add(season);
            }

            this.seasonRepository.SaveChanges();
        }

        private void SeedCountriesData()
        {
            var country = SeedDataTests.GetCountriesData();

            this.countryRepository.Add(country);

            this.countryRepository.SaveChanges();
        }

        private void SeedTeamsData()
        {
            var teams = SeedDataTests.GetTeamsData();
            foreach (var team in teams)
            {
                this.teamRepository.Add(team);
            }

            this.teamRepository.SaveChanges();

        }

        private void SeedTeamsLeaguesData()
        {
            var teamLeagues = SeedDataTests.GetTeamLeaguesData();

            foreach (var teamLeague in teamLeagues)
            {
                this.teamLeagueRepository.Add(teamLeague);
            }

            this.teamLeagueRepository.SaveChanges();

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
            this.teamRepository = new EfDeletableEntityRepository<Team>(context);
            this.teamLeagueRepository = new EfDeletableEntityRepository<TeamLeagues>(context);
        }

        private void InitializeApiModel()
        {
            this.ApiModel = new ImportLeaguesApi()
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
        }

        private void InitializeMapper() => AutoMapperConfig.
           RegisterMappings(Assembly.Load("SoccerSolutionsApp.Web.ViewModels"));


    }
}
