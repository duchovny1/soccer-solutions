namespace SoccerSolutionsApp.Services.Data.Users
{
    using SoccerSolutionsApp.Data.Models;
    using System.Threading.Tasks;

    public interface IUsersService
    {
        Task FollowUserAsync(string userToFollow, string userFollowing);
    }
}
