namespace SoccerSolutionsApp.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using SoccerSolutionsApp.Data;
    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Data.Repositories;
    using SoccerSolutionsApp.Services.Data.H2H;
    using SoccerSolutionsApp.Services.Mapping;
    using Xunit;

    public class H2HServiceTests
    {
        private readonly H2HService h2hService;
        private EfDeletableEntityRepository<Fixture> fixturesRepository;
        private EfDeletableEntityRepository<Team> teamsRepository;

        public H2HServiceTests()
        {
            this.InitializeDatabaseAndRepositories();
            this.InitializeMapper();
            this.h2hService = new H2HService(this.fixturesRepository);
        }

        [Fact]
        public async Task TestHomeWinsAwayWinsWAndDrawsMethodsIfAreWorkingCorrectly()
        {
            this.SeedTestData();
            var h2hService = new H2HService(this.fixturesRepository);

            var homeWins = await this.h2hService.HomeTeamWins(1, 2);
            var awayWins = await this.h2hService.AwayTeamWins(1, 2);
            var draws = await this.h2hService.AwayTeamWins(1, 2);

            Assert.Equal(2, homeWins);
            Assert.Equal(1, awayWins);
            Assert.Equal(1, draws);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(5, 2)]
        public async Task TestHomeWinsMethodIfReturnsNullWithIncorrectData(int homeTeamId, int awayTeamId)
        {
            this.SeedTestData();

            var homeWins = await this.h2hService.HomeTeamWins(homeTeamId, awayTeamId);
            var awayWins = await this.h2hService.AwayTeamWins(homeTeamId, awayTeamId);
            var draws = await this.h2hService.Draws(homeTeamId, awayTeamId);

            Assert.Equal(0, homeWins);
            Assert.Equal(0, awayWins);
            Assert.Equal(0, draws);
        }

        [Theory]
        [InlineData(1, 2)]
        public async Task TestTotalBothTeamsScoredInLast20GamesIfWorksCorrecyly(int teamOne, int teamTwo)
        {
            this.SeedTestData();

            var homeTeam = await this.h2hService.TotalBothTeamsScoredInLast20Games(teamOne);
            var awayTeam = await this.h2hService.TotalBothTeamsScoredInLast20Games(teamTwo);

            Assert.Equal(1, homeTeam);
            Assert.Equal(1, awayTeam);
        }


        [Theory]
        [InlineData(0, 0)]
        public async Task TestTotalBothTeamsScoredInLast20GamesWithIncorrectData(int teamOne, int teamTwo)
        {
            this.SeedTestData();

            var homeTeam = await this.h2hService.TotalBothTeamsScoredInLast20Games(teamOne);
            var awayTeam = await this.h2hService.TotalBothTeamsScoredInLast20Games(teamTwo);

            Assert.Equal(0, homeTeam);
            Assert.Equal(0, awayTeam);
        }


        [Theory]
        [InlineData(1, 2)]
        public async Task TestOver2point5InLast20GamesMethodIfWorksCorrectly(int teamOne, int teamTwo)
        {
            this.SeedTestData();

            var homeTeam = await this.h2hService.TotalOver2point5GoalsInLast20Games(teamOne);
            var awayTeam = await this.h2hService.TotalOver2point5GoalsInLast20Games(teamTwo);

            Assert.Equal(2, homeTeam);
            Assert.Equal(2, awayTeam);
        }

        [Theory]
        [InlineData(0, 0)]
        public async Task TestOver2point5InLast20GamesMethodWithIncorrectData(int teamOne, int teamTwo)
        {
            this.SeedTestData();

            var homeTeam = await this.h2hService.TotalOver2point5GoalsInLast20Games(teamOne);
            var awayTeam = await this.h2hService.TotalOver2point5GoalsInLast20Games(teamTwo);

            Assert.Equal(0, homeTeam);
            Assert.Equal(0, awayTeam);
        }

        [Theory]
        [InlineData(1, 2)]
        public async Task TestTotalGoalsHomeTeamScored(int teamOne, int teamTwo)
        {
            this.SeedTestData();

            var teamOneGoalsAsHomeTeam = await this.h2hService.TotalGoalsHomeTeamScored(teamOne, teamTwo);
            var teamTwoGoalsAsHomeTeam = await this.h2hService.TotalGoalsHomeTeamScored(teamTwo, teamOne);

            Assert.Equal(7, teamOneGoalsAsHomeTeam);
            Assert.Equal(3, teamTwoGoalsAsHomeTeam);
        }

        private void InitializeDatabaseAndRepositories()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var context = new ApplicationDbContext(options);

            this.fixturesRepository = new EfDeletableEntityRepository<Fixture>(context);
            this.teamsRepository = new EfDeletableEntityRepository<Team>(context);
        }

        private void SeedTestData()
        {

            var fixtures = SeedDataTests.GetFixturesData();

            foreach (var fixture in fixtures)
            {
                this.fixturesRepository.Add(fixture);
            }

            this.fixturesRepository.SaveChanges();
        }

        private void InitializeMapper() => AutoMapperConfig.
           RegisterMappings(Assembly.Load("SoccerSolutionsApp.Web.ViewModels"));
    }
}
