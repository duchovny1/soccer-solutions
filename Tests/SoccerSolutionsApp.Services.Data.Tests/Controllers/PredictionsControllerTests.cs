namespace SoccerSolutionsApp.Services.Data.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using MyTested.AspNetCore.Mvc;
    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Services.Data.Predictions;
    using SoccerSolutionsApp.Services.Data.Tests.Data;
    using SoccerSolutionsApp.Services.Mapping;
    using SoccerSolutionsApp.Web.Controllers;
    using SoccerSolutionsApp.Web.ViewModels.Predictions;
    using SoccerSolutionsApp.Web.ViewModels.Teams;
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
        public void CreateGetShouldReturnRightModelsAndRightCountriesCount()
        => MyController<PredictionsController>
                  .Instance()
                  .WithUser()
                  .WithData(data => data
                         .WithEntities(entity => entity.AddRange(
                             new Country { Name = "England" },
                             new Country { Name = "Finland" },
                             new Country { Name = "Russia" })))
                  .Calling(x => x.Create())
                  .ShouldReturn()
                  .View(viewResult => viewResult
                  .WithModelOfType<CreatePredictionInputViewModel>()
                  .Passing(x =>
                  {
                      Assert.Equal(3, x.Countries.Count());
                      Assert.IsType<List<CountriesDropDownViewModel>>(x.Countries);
                  }));

        [Fact]
        public void CreatePostShouldRedirectWithInvalidModelState()
        {
            var model = new CreatePredictionInputViewModel()
            {
                FixtureId = 10,
                Content = "asdada",
            };

            MyController<PredictionsController>
                .Instance()
                .WithSetup(controller => controller.ModelState
                    .AddModelError("SomeError", "Error Message"))
                .Calling(x => x.Create(model))
                .ShouldReturn()
                .Redirect(redirect => redirect
                     .To<PredictionsController>(action => action.All()));
        }

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
