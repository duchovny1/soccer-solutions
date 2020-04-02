namespace SoccerSolutionsApp.Services.Data.Predictions
{
    using System.Collections.Generic;
    using System.Linq;
    using SoccerSolutionsApp.Data.Common.Repositories;
    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Services.Mapping;

    public class PredictionsService : IPredictionsService
    {
        private readonly IRepository<Prediction> predictionsRepository;

        public PredictionsService(IRepository<Prediction> predictionsRepository)
        {
            this.predictionsRepository = predictionsRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            var predictions = this.predictionsRepository.All().OrderBy(x => x.CreatedOn);

            return predictions.To<T>().ToList();
        }
    }
}
