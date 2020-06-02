namespace SoccerSolutionsApp.Services.Data.Tests.Services
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using MyTested.AspNetCore.Mvc;
    using SoccerSolutionsApp.Data;
    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Data.Repositories;
    using SoccerSolutionsApp.Services.Data.Predictions;
    using SoccerSolutionsApp.Services.Data.Tests.Data;
    using SoccerSolutionsApp.Services.Data.Users;
    using SoccerSolutionsApp.Services.Mapping;
    using SoccerSolutionsApp.Web.ViewModels;
    using SoccerSolutionsApp.Web.ViewModels.Predictions;
    using Xunit;

    public class PredictionsServiceTest
    {
        private readonly IPredictionsService predictionsService;

        private EfDeletableEntityRepository<Prediction> predictionsRepository;
        private EfDeletableEntityRepository<Fixture> fixtureRepository;
        private EfDeletableEntityRepository<Following> followingRepository;
        private EfDeletableEntityRepository<ApplicationUser> usersRepository;

        private ApplicationDbContext dbContext;

        public PredictionsServiceTest()
        {
            this.InitializeDbAndRepositories();

            this.predictionsService = new PredictionsService(
                this.predictionsRepository,
                this.fixtureRepository,
                this.followingRepository,
                this.usersRepository);

            this.SeedDatabase();
        }

        [Fact]
        public async Task GetAllShouldWorksCorrectly()
        {
            var result = await this.predictionsService.GetAll<PredictionsListingViewModel>();
            var predictionsCount = result.Count();

            Assert.NotNull(result);
            Assert.Equal(5, predictionsCount);
        }

        [Fact]
        public async Task TestIfCreateAPredictionsWorksCorrectly()
        {
            var model = new CreatePredictionInputViewModel()
            {
                FixtureId = 5,
                Content = "asdajidasda",
                Prediction = "1",
                LeagueId = 2,
                CountryId = 1,
            };

            var userId = await this.dbContext.Users.Select(x => x.Id).FirstOrDefaultAsync();

            var predictionId = await this.predictionsService.CreateAsync(model, userId);
            var prediction = await this.predictionsRepository.All().FirstOrDefaultAsync(x => x.Id == predictionId);

            Assert.NotNull(prediction);
            Assert.Equal(predictionId, prediction.Id);
        }

        [Fact]
        public async Task TestIfGetUserPredictionsReturnsCorrectPredictions()
        {
            var userOne = await this.usersRepository.All().FirstOrDefaultAsync(x => x.UserName == "testuser@abv.bg");
            var userTwo = await this.usersRepository.All().FirstOrDefaultAsync(x => x.UserName == "anothertestuser@abv.bg");

            var user = new ApplicationUser()
            {
                UserName = "userFollowing@abv.bg",
            };

            this.dbContext.Users.Add(user);
            this.dbContext.SaveChanges();

            var userFollowing = user.Id;

            var userService = new UserService(this.followingRepository, this.usersRepository);
            await userService.FollowUserAsync(userOne.Id, userFollowing);
            await userService.FollowUserAsync(userTwo.Id, userFollowing);

            var result = this.predictionsService.GetFollowingsPredictions(userFollowing);

            Assert.Equal(5, result.Count());
        }

        [Fact]
        public async Task GetPredictionsByIdShouldReturnCorrectPrediction()
        {
            var prediction = await this.predictionsRepository.All().FirstOrDefaultAsync();

            var predictionById = await this.predictionsService.GetPredictionById(prediction.Id);

            Assert.NotNull(predictionById);
            Assert.Equal(prediction.Id, predictionById.Id);
        }

        [Fact]
        public async Task GetPredictionsByIdShouldReturnNullWithIncorrectData()
        {
            var predictionsAll = await this.predictionsService.GetAll<PredictionsListingViewModel>();
            var predictionById = await this.predictionsService.GetPredictionById(int.MaxValue);

            Assert.True(predictionsAll.Count() > 0);
            Assert.Null(predictionById);
        }

        [Fact]
        public async Task GetUserPredictionsShouldReturnRightPredictions()
        {
            var userOne = await this.usersRepository.All().FirstOrDefaultAsync(x => x.UserName == "testuser@abv.bg");
            var predictionsForUserOne = await this.predictionsService.GetUserPredictions(userOne.Id);

            Assert.Equal(2, predictionsForUserOne.Count());

            var userTwo = await this.usersRepository.All().FirstOrDefaultAsync(x => x.UserName == "anothertestuser@abv.bg");
            var predictionsForUserTwo = await this.predictionsService.GetUserPredictions(userTwo.Id);

            Assert.Equal(3, predictionsForUserTwo.Count());
        }

        private void InitializeDbAndRepositories()
        {
            ApplicationDbContextTesting connection = new ApplicationDbContextTesting();
            var dbContext = connection.DbContext;
            this.dbContext = dbContext;

            this.predictionsRepository = new EfDeletableEntityRepository<Prediction>(dbContext);
            this.fixtureRepository = new EfDeletableEntityRepository<Fixture>(dbContext);
            this.followingRepository = new EfDeletableEntityRepository<Following>(dbContext);
            this.usersRepository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
        }

        private void SeedDatabase()
        {
            var season = SeedDataTests.GetSeasonData();
            this.dbContext.Seasons.Add(season);

            var country = SeedDataTests.GetCountriesData();
            this.dbContext.Countries.Add(country);

            var league = SeedDataTests.GetLeaguesData();
            this.dbContext.Leagues.Add(league);

            var teams = SeedDataTests.GetTeamsData();
            this.dbContext.Teams.AddRange(teams);

            var fixtures = SeedDataTests.GetFixturesData();
            this.dbContext.Fixtures.AddRange(fixtures);

            var users = SeedDataTests.GetUsers();
            this.dbContext.Users.AddRange(users);

            this.dbContext.SaveChanges();

            var userOne = this.usersRepository.All().FirstOrDefault(x => x.UserName == "testuser@abv.bg").Id;
            var userTwo = this.usersRepository.All().FirstOrDefault(x => x.UserName == "anothertestuser@abv.bg").Id;

            var predictions = SeedDataTests.GetPredictionsDataForUserOne(userOne);
            this.dbContext.Predictions.AddRange(predictions);


            var predictions2 = SeedDataTests.GetPredictionsDataForUserTwo(userTwo);
            this.dbContext.Predictions.AddRange(predictions2);

            this.dbContext.SaveChanges();
        }

    }
}
