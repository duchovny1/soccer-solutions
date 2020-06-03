namespace SoccerSolutionsApp.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SoccerSolutionsApp.Common;
    using SoccerSolutionsApp.Services.Data;
    using SoccerSolutionsApp.Services.Data.Administrations;
    using SoccerSolutionsApp.Services.Data.Users;
    using SoccerSolutionsApp.Web.Areas.Administration.ViewModels;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class DashboardController : AdministrationController
    {
        private readonly IAdminInfoService adminInfoService;

        public DashboardController(IAdminInfoService adminInfoService)
        {
            this.adminInfoService = adminInfoService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new IndexViewModel()
            {
                UsersCount = await this.adminInfoService.CountUsersAsync(),
                ActiveUsers = await this.adminInfoService.ActiveUsersAsync(),
            };

            return this.View(viewModel);
        }
      
    }
}
