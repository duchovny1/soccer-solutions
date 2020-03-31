namespace SoccerSolutionsApp.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SoccerSolutionsApp.Services.Predictions;
    using SoccerSolutionsApp.Web.ViewModels.Predictions;

    public class PredictionsController : Controller
    {
        private readonly IPredictionsService predictionsService;

        public PredictionsController(IPredictionsService predictionsService)
        {
            this.predictionsService = predictionsService;
        }

        public IActionResult All()
        {
            var predictions = this.predictionsService.GetAll<PredictionsListingViewModel>();

            return this.View(predictions);
        }
    }
}