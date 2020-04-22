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
       

        public PredictionsService(
            IRepository<Prediction> predictionsRepository,
            IDeletableEntityRepository<Fixture> fixturesRepository)
        {
            this.predictionsRepository = predictionsRepository;
            this.fixturesRepository = fixturesRepository;
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
        {
            var predictions = await this.predictionsRepository.All()
                .OrderBy(x => x.CreatedOn).To<T>().ToListAsync();

            return predictions;
        }

        public async Task<IEnumerable<UserPredictionsViewModel>> GetUserPredictions(ApplicationUser user)
         => await this.predictionsRepository.All().Where(x => x.User == user).OrderBy(x => x.CreatedOn).
                To<UserPredictionsViewModel>().ToListAsync();

    }
}
