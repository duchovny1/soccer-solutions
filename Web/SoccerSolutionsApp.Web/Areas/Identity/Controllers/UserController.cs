namespace SoccerSolutionsApp.Web.Areas.Identity.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Services.Data.Predictions;
    using SoccerSolutionsApp.Services.Data.Users;
    using SoccerSolutionsApp.Web.ViewModels.User;

    [Area("Identity")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IPredictionsService predictionsService;
        private readonly IUsersService usersService;

        public UserController(
            UserManager<ApplicationUser> userManager,
            IPredictionsService predictionsService,
            IUsersService usersService)
        {
            this.userManager = userManager;
            this.predictionsService = predictionsService;
            this.usersService = usersService;
        }

        public async Task<IActionResult> ById(string userName)
        {
            var user = await this.userManager.FindByNameAsync(userName);

            var viewModel = new UserInfoViewModel
            {
                UserUsername = user.UserName,
                UserPredictions = await this.predictionsService.GetUserPredictions(user.Id),
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> AllUsers()
        {
            var viewModel = await this.usersService.GetAllUsersAsync();
            return this.View(viewModel);
        }

    }
}