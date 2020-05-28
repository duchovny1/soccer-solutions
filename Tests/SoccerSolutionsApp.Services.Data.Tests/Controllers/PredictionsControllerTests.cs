namespace SoccerSolutionsApp.Services.Data.Tests.Controllers
{
    using Moq;
    using MyTested.AspNetCore.Mvc;
    using SoccerSolutionsApp.Services.Data.Predictions;
    using SoccerSolutionsApp.Services.Data.Tests.Data;
    using SoccerSolutionsApp.Services.Mapping;
    using SoccerSolutionsApp.Web.Controllers;
    using SoccerSolutionsApp.Web.ViewModels.Predictions;
    using System;
    using System.Reflection;
    using System.Threading.Tasks;
    using Xunit;

    public class PredictionsControllerTests
    {
        public PredictionsControllerTests()
        {
            this.InitializerMapper();
        }

        [Fact]
        public void CreateGETPredictionMustBeOnlyForAuthorizedUsers()
        =>
            MyController<PredictionsController>
                .Calling(x => x.Create())
                .ShouldHave()
                .ActionAttributes(attrs => attrs
               .RestrictingForHttpMethod(HttpMethod.Get)
               .RestrictingForAuthorizedRequests());


        [Fact]
        public void CreatePOSTMustByOnlyForAuthorizedUser()
        =>
            MyController<PredictionsController>
                .Calling(x => x.Create(With.Default<CreatePredictionInputViewModel>()))
                .ShouldHave()
                .ActionAttributes(attrs => attrs
                .RestrictingForHttpMethod(HttpMethod.Post)
                .RestrictingForAuthorizedRequests());

        [Fact]
        public void PredictionsByIdShouldReturnNotFoundWithWrongId()
        => MyController<PredictionsController>
             .Calling(c => c.ById(int.MaxValue))
            .ShouldReturn()
            .NotFound();


        [Fact]
        public void PredictionsByIdShouldReturnCorrectModel()
        {
            MyController<PredictionsController>
               .Instance(instance => instance
                      .WithUser("Author 1")
                      .WithData(PredictionTestData.GetPredictions(2)))
            .Calling(c => c.ById(1))
            .ShouldReturn()
            .View(view => view
            .WithModelOfType<PredictionsListingViewModel>());
        }

        [Fact]
        public void PredictionsAllShouldReturnViewWithPredictions()
        {
            MyController<PredictionsController>
                .Instance()
                .Calling(x => x.All())
                .ShouldReturn()
                .View(view =>
                      view.WithModelOfType<PredictionsListingAndFollowingsViewModel>());
        }

        [Fact]
        public void CreateGetShouldReturnCorrectViewModel()
        {
            MyController<PredictionsController>
                 .Instance(x => x.WithUser("1", "admin@abv.bg"))
                 .Calling(x => x.Create())
                 .ShouldHave()
                 .ValidModelState()
                 .AndAlso()
                 .ShouldReturn()
                 .View(x => x.WithModelOfType<CreatePredictionInputViewModel>());
        }

        private void InitializerMapper() => AutoMapperConfig.
        RegisterMappings(Assembly.Load("SoccerSolutionsApp.Web.ViewModels"));

    }
}
