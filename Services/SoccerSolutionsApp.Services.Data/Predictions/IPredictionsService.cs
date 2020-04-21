namespace SoccerSolutionsApp.Services.Data.Predictions
{
    using SoccerSolutionsApp.Web.ViewModels.Predictions;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPredictionsService
    {
        Task<IEnumerable<T>> GetAll<T>();

        Task CreateAsync(CreatePredictionInputViewModel model);
    }
}
