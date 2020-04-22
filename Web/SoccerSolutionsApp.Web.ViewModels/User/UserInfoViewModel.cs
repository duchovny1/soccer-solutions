using System.Collections.Generic;

namespace SoccerSolutionsApp.Web.ViewModels.User
{
    public class UserInfoViewModel
    {
        // 
        public string UserUsername { get; set; }

        public double SuccessRate { get; set; }

        public int HowManyPredictsAreTrue { get; set; }

        public int HowManyPredictsAreFalse { get; set; }

        public IEnumerable<UserPredictionsViewModel> UserPredictions { get; set; }
    }
}
