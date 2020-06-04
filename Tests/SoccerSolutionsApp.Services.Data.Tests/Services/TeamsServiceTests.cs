namespace SoccerSolutionsApp.Services.Data.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;
    using SoccerSolutionsApp.Data;
    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Data.Repositories;
    using SoccerSolutionsApp.Services.Data.Teams;
    using SoccerSolutionsApp.Services.Data.TeamsServices;
    using SoccerSolutionsApp.Services.Mapping;
    using SoccerSolutionsApp.Web.ViewModels.Teams;
    using Xunit;

    public class TeamsServiceTests
    {
        private ITeamsService teamsService;

        private ApplicationDbContext dbContext;

        private EfDeletableEntityRepository<Team> teamsRepository;
        private EfDeletableEntityRepository<League> leagueRepository;
        private EfDeletableEntityRepository<Country> countryRepository;
        private EfDeletableEntityRepository<TeamLeagues> teamLeaguesRepository;

        private ImportTeamsApi apiModel;

        public TeamsServiceTests()
        {
            this.InitializeDatabaseAndRepositories();
            this.InitializeMapper();

            this.teamsService = new TeamsService(
                this.teamsRepository,
                this.leagueRepository,
                this.countryRepository,
                this.teamLeaguesRepository);

            this.InitializeApiModel();

            this.SeedDatabase();
        }

        [Fact]
        public async Task GetAllShouldReturnRightTeams()
        {
            var result = await this.teamsService.GetAll<TeamListingViewModel>();
            var teamsCount = result.Count();

            Assert.NotNull(result);
            Assert.Equal(2, teamsCount);
        }

        [Fact]
        public async Task CreateShouldCreateTeamAndTeamLeagues()
        {
            int countTeams = await this.teamsRepository.All().CountAsync();
            this.teamsService.Create(this.apiModel, 1);
            int countAfterAddingNewTeam = await this.teamsRepository.All().CountAsync();
            var teams = this.teamsRepository.All();

            Assert.True(countAfterAddingNewTeam == countTeams + 1);
            Assert.True(teams.Any(x => x.Name == "Manchester United"));

            var isTeamLeaguesTableCreated = this.teamLeaguesRepository.All().Any(x => x.LeagueId == 1 && x.TeamId == 3);

            Assert.True(isTeamLeaguesTableCreated);
        }

        [Fact]
        public async Task CreateWithExistingTeamShouldCreateTeamLeaguesRecord()
        {
             this.teamsService.Create(this.apiModel, 1);
            // 3 is manchester united's id
             bool isTeamCreated = this.teamsRepository.All().Any(x => x.Id == 3);
             Assert.True(isTeamCreated);

            // first add manchester in premier league
             int countBefore = this.teamsRepository.All()
                .Where(x => x.Name == "Manchester United")
                .SelectMany(x => x.TeamLeagues)
                .Count();

             this.teamsService.Create(this.apiModel, 2);

            // then add manchester(already existing) in championship(new league)
             int countAfter = this.teamsRepository.All()
                .Where(x => x.Name == "Manchester United")
                .SelectMany(x => x.TeamLeagues)
                .Count();

            // should make new record in team leagues
             Assert.False(countBefore == countAfter);

             var isTeamLeaguesTableCreated = this.teamLeaguesRepository.All().Any(x => x.LeagueId == 2 && x.TeamId == 3);
             Assert.True(isTeamLeaguesTableCreated);
        }

        [Fact]
        public async Task IfTeamIsAlreadyCreatedShouldDoNothing()
        {
            this.teamsService.Create(this.apiModel, 1);

            int countBefore = this.teamsRepository.All()
                 .Where(x => x.Name == "Manchester United")
                 .SelectMany(x => x.TeamLeagues)
                 .Count();

            this.teamsService.Create(this.apiModel, 1);

            int countAfter = this.teamsRepository.All()
                .Where(x => x.Name == "Manchester United")
                .SelectMany(x => x.TeamLeagues)
                .Count();

            Assert.True(countBefore == countAfter);
        }

        [Fact]
        public async Task ShouldNotAddTeamWithNonsxistingLeagueId()
        {
            int countTeams = await this.teamsRepository.All().CountAsync();
            this.teamsService.Create(this.apiModel, 5);
            int countAfterAddingNewTeam = await this.teamsRepository.All().CountAsync();
            var teams = this.teamsRepository.All();

            Assert.True(countAfterAddingNewTeam == countTeams);
            Assert.False(teams.Any(x => x.Name == "Manchester United"));
        }

        private void InitializeApiModel()
        {
            this.apiModel = new ImportTeamsApi
            {
                Api = new ImportTeamsInputModel
                {
                    Results = 1,
                    Teams = new ImportTeamInputModel[]
                    {
                        new ImportTeamInputModel
                        {
                            TeamId = 3,
                            Name = "Manchester United",
                            Code = "MU",
                            IsNational = false,
                            Country = "England",
                        },
                    },
                },
            };
        }

        private void InitializeMapper() => AutoMapperConfig.
             RegisterMappings(Assembly.Load("SoccerSolutionsApp.Web.ViewModels"));

        private void InitializeDatabaseAndRepositories()
        {
            ApplicationDbContextTesting connection = new ApplicationDbContextTesting();
            var dbContext = connection.DbContext;
            this.dbContext = dbContext;

            this.teamsRepository = new EfDeletableEntityRepository<Team>(dbContext);
            this.leagueRepository = new EfDeletableEntityRepository<League>(dbContext);
            this.countryRepository = new EfDeletableEntityRepository<Country>(dbContext);
            this.teamLeaguesRepository = new EfDeletableEntityRepository<TeamLeagues>(dbContext);
        }

        private void SeedDatabase()
        {
            var country = SeedDataTests.GetCountriesData();
            this.dbContext.Countries.Add(country);

            var seasons = SeedDataTests.GetSeasonData();
            this.dbContext.Seasons.AddRange(seasons);

            var leagues = SeedDataTests.GetLeaguesData();
            this.dbContext.Leagues.AddRange(leagues);

            var teams = SeedDataTests.GetTeamsData();
            this.dbContext.Teams.AddRange(teams);


            this.dbContext.SaveChanges();
        }

    }
}
