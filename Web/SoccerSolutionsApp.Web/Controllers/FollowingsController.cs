namespace SoccerSolutionsApp.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Services.Data.Users;

    public class FollowingsController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUsersService usersService;

        public FollowingsController(
            UserManager<ApplicationUser> userManager,
            IUsersService usersService)
        {
            this.userManager = userManager;
            this.usersService = usersService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> FollowUser(string userToFollowId)
        {
            var currentUserId = this.userManager.GetUserId(this.User);

            if (userToFollowId == null || currentUserId == null || currentUserId == userToFollowId)
            {
                return this.BadRequest();
            }

            await this.usersService.FollowUserAsync(userToFollowId, currentUserId);

            return this.Json(userToFollowId);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UnfollowUser(string userToFollowId)
        {
            var currentUserId = this.userManager.GetUserId(this.User);

            if (userToFollowId == null || currentUserId == null || currentUserId == userToFollowId)
            {
                return this.BadRequest();
            }

            await this.usersService.UnfollowUserAsync(userToFollowId, currentUserId);

            return this.Json(userToFollowId);
        }

    }
}