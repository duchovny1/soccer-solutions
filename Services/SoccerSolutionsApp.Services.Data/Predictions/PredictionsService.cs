namespace SoccerSolutionsApp.Services.Data.Predictions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using SoccerSolutionsApp.Data.Common.Repositories;
    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Services.Mapping;
    using SoccerSolutionsApp.Web.ViewModels.Predictions;
    using SoccerSolutionsApp.Web.ViewModels.User;

    public class PredictionsService : IPredictionsService
    {
        private readonly IRepository<Prediction> predictionsRepository;
        private readonly IDeletableEntityRepository<Fixture> fixturesRepository;
        private readonly IDeletableEntityRepository<Following> followingsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public PredictionsService(
            IRepository<Prediction> predictionsRepository,
            IDeletableEntityRepository<Fixture> fixturesRepository,
            IDeletableEntityRepository<Following> followingsRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.predictionsRepository = predictionsRepository;
            this.fixturesRepository = fixturesRepository;
            this.followingsRepository = followingsRepository;
            this.userRepository = userRepository;
        }

        public async Task CreateAsync(CreatePredictionInputViewModel model, string userId)
        {

            Prediction prediction = new Prediction()
            {
                FixtureId = model.FixtureId,
                Content = model.Content,
                GamePrediction = model.Prediction,
                UserId = userId,
                IsMatchFinished = false,
            };

            await this.predictionsRepository.AddAsync(prediction);
            await this.predictionsRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll<T>()
            => await this.predictionsRepository.All()
               .OrderBy(x => x.CreatedOn).To<T>().ToListAsync();

        public async Task<IEnumerable<UserPredictionsViewModel>> GetUserPredictions(string userId)
         => await this.predictionsRepository.All().Where(x => x.UserId == userId).OrderBy(x => x.CreatedOn).
                To<UserPredictionsViewModel>().ToListAsync();

        public async Task<IEnumerable<PredictionsListingViewModel>> GetFollowingsPredictions(string userId)
        {
            // get all users that current user is following

            var users = this.followingsRepository.All()
                .Where(x => x.UserFollowingId == userId)
                .Select(x => x.UserToFollowId);

            var allPredictions = new List<PredictionsListingViewModel>();

            foreach (var id in users)
            {
                var predictionsForSingleUser = await this.predictionsRepository.All().Where(x => x.UserId == id)
                    .To<PredictionsListingViewModel>().ToListAsync();

                 allPredictions.AddRange(predictionsForSingleUser);
            }

             return allPredictions.AsQueryable().OrderByDescending(x => x.FixtureKickOff).ToList();
        }
       
    }
}
