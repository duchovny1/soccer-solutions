namespace SoccerSolutionsApp.Web.Areas.Identity.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Services.Data.Predictions;
    using SoccerSolutionsApp.Web.ViewModels.User;

    [Area("Identity")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IPredictionsService predictionsService;

        public UserController(
            UserManager<ApplicationUser> userManager,
            IPredictionsService predictionsService)
        {
            this.userManager = userManager;
            this.predictionsService = predictionsService;
        }

        public async Task<IActionResult> ById(string userName)
        {
            var user = await this.userManager.FindByNameAsync(userName);

            var viewModel = new UserInfoViewModel
            {
                UserUsername = user.UserName,
                UserPredictions = await this.predictionsService.GetUserPredictions(user),
            };

            return this.View(viewModel);
        }
    }
}