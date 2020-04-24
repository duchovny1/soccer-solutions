namespace SoccerSolutionsApp.Web.ViewModels.Predictions
{
    using System.Collections.Generic;


    public class PredictionsListingAndFollowingsViewModel
    {
        public IEnumerable<PredictionsListingViewModel> Predictions { get; set; }

        public IEnumerable<string> CurrentUserFollowings { get; set; }
    }
}
