namespace SoccerSolutionsApp.Services.Data.Tests.Controllers
{
    using Moq;
    using MyTested.AspNetCore.Mvc;
    using SoccerSolutionsApp.Services.Data.Predictions;
    using SoccerSolutionsApp.Services.Data.Tests.Data;
    using SoccerSolutionsApp.Web.Controllers;
    using SoccerSolutionsApp.Web.ViewModels.Predictions;
    using System.Threading.Tasks;
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
        {
            MyController<PredictionsController>
               .Instance(instance => instance
               .WithData(PredictionTestData.GetPredictions(1)))
            .Calling(c => c.ById(1))
            .ShouldReturn()
            .View(view => view
            .WithModelOfType<PredictionsListingViewModel>()
            .Passing(prediction => prediction.Id == 1));

        }


        [Fact]
        public void PredictionsAllShouldReturnViewWithPredictions()
        {
            var repo = new Mock<IPredictionsService>();
            repo.Setup(r => r.GetAll<PredictionsListingViewModel>())
                .Returns(Task.FromResult(PredictionTestData.GetPredictions(1)));

            MyController<PredictionsController>
                .Instance()
                .WithDependencies(x => x.
                     With<IPredictionsService>(repo.Object))
                .Calling(x => x.All())
                .ShouldReturn()
                .View();
        }

    }
}
