namespace SoccerSolutionsApp.Services.Data.Tests.Controllers
{
    using MyTested.AspNetCore.Mvc;
    using SoccerSolutionsApp.Services.Data.Tests.Data;
    using SoccerSolutionsApp.Web.Controllers;
    using SoccerSolutionsApp.Web.ViewModels.Predictions;
    using Xunit;

    public class PredictionsControllerTests
    {
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
            => MyController<PredictionsController>
               .Instance(instance => instance
               .WithData(PredictionTestData.GetPredictions(1)))
            .Calling(c => c.ById(1))
            .ShouldReturn()
            .View(view => view
            .WithModelOfType<PredictionsListingViewModel>()
            .Passing(prediction => prediction.Id == 1));

    }
}
