namespace SoccerSolutionsApp.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using SoccerSolutionsApp.Data.Models;

    public class UsersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await SeedUsersAsync(userManager, "admin@abv.bg", "1234567");
            await SeedUsersAsync(userManager, "moderator@abv.bg", "1234567");
        }

        private static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager, string email, string password)
        {
            var user = await userManager.FindByNameAsync(email);

            if (user == null)
            {
                var newUser = new ApplicationUser()
                {
                    UserName = email,
                    Email = email,
                };

                var result = await userManager.CreateAsync(newUser, password);

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
