namespace SoccerSolutionsApp.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using SoccerSolutionsApp.Common;
    using SoccerSolutionsApp.Data.Models;

    public class UsersToRoles : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await SeedUserToRoles(roleManager, userManager, "admin@abv.bg", GlobalConstants.AdministratorRoleName);
            await SeedUserToRoles(roleManager, userManager, "moderator@abv.bg", GlobalConstants.ModeratorRoleName);
        }

        private static async Task SeedUserToRoles(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager, string userName, string roleName)
        {
            var role = await roleManager.RoleExistsAsync(roleName);
            var user = await userManager.FindByNameAsync(userName);

            if (role && user != null)
            {
                var result = await userManager.AddToRoleAsync(user, roleName);

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
