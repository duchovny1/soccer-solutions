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

        public async Task CreateAsync(CreatePredictionInputViewModel model)
        {
            //var fixture = this.fixturesRepository.All().FirstOrDefaultAsync(x => x.Id == model.EventId);

            //if (fixture != null)
            //{
                Prediction prediction = new Prediction()
                {
                    //Title = model.Title,
                    //Content = model.Content,
                    //EventId = model.EventId,
                    //GamePrediction = model.Prediction,
                    //HomeTeamLogo = model.HomeTeamLogo,
                    //AwayTeamLogo = model.AwayTeamLogo,
                };

                await this.predictionsRepository.AddAsync(prediction);
                await this.predictionsRepository.SaveChangesAsync();
           // }
            //else
            //{
                //throw new InvalidOperationException("The game you're trying to put predictions, it does not exist");
            //}
        }

        public async Task<IEnumerable<T>> GetAll<T>()
        {
            var predictions = await this.predictionsRepository.All()
                .OrderBy(x => x.CreatedOn).To<T>().ToListAsync();

            return predictions;
        }
    }
}
