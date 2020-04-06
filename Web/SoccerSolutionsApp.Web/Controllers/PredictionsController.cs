namespace SoccerSolutionsApp.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SoccerSolutionsApp.Services.Data.Predictions;
    using SoccerSolutionsApp.Web.ViewModels.Predictions;

    public class PredictionsController : Controller
    {
        private readonly IPredictionsService predictionsService;

        public PredictionsController(IPredictionsService predictionsService)
        {
            this.predictionsService = predictionsService;
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(PredictionsInputViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                await this.predictionsService.CreateAsync(model);
                return this.View();
            }

            return this.RedirectToAction(nameof(this.All));
        }

        public IActionResult All()
        {
            var predictions = this.predictionsService.GetAll<PredictionsListingViewModel>();

            return this.View(predictions);
        }
    }
}