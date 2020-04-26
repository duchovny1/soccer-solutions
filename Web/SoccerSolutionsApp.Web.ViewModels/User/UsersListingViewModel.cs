namespace SoccerSolutionsApp.Web.ViewModels.User
{
    using System;
    using System.Linq;

    using AutoMapper;
    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Services.Mapping;

    public class UsersListingViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string Username { get; set; }

        public DateTime CreatedOn { get; set; }

        public string RegisterOn => this.CreatedOn.ToString("d");

        public int PredictionsCount { get; set; }

        public int TruePredictions { get; set; }

        public double SuccessRate => ((double)this.TruePredictions / (double)this.PredictionsCount) * 100;

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, UsersListingViewModel>()
                 .ForMember(x => x.PredictionsCount, y => y.MapFrom(z => z.Predictions
                 .Where(pr => pr.IsPredictionTrue == true).Count()));
        }
    }
}
