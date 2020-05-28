namespace SoccerSolutionsApp.Services.Data.Predictions
{
    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Web.ViewModels.Predictions;
    using SoccerSolutionsApp.Web.ViewModels.User;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IPredictionsService
    {
        Task<IEnumerable<T>> GetAll<T>();

        Task<int> CreateAsync(CreatePredictionInputViewModel model, string userId);

        Task<IEnumerable<UserPredictionsViewModel>> GetUserPredictions(string userId);

        IEnumerable<PredictionsListingViewModel> GetFollowingsPredictions(string userId);

        Task<PredictionsListingViewModel> GetPredictionById(int id);
    }
}
