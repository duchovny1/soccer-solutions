namespace SoccerSolutionsApp.Services.Data.Users
{
    using System.Threading.Tasks;

    using SoccerSolutionsApp.Data.Common.Repositories;
    using SoccerSolutionsApp.Data.Models;

    public class UserService : IUsersService
    {
        private readonly IDeletableEntityRepository<Following> followingsRepository;

        public UserService(IDeletableEntityRepository<Following> followingsRepository)
        {
            this.followingsRepository = followingsRepository;
        }

        public async Task FollowUserAsync(string userToFollow, string userFollowing)
        {
            if (userToFollow != null & userFollowing != null)
            {
                Following following = new Following()
                {
                    UserToFollowId = userToFollow,
                    UserFollowingId = userFollowing,
                };

                await this.followingsRepository.AddAsync(following);
                await this.followingsRepository.SaveChangesAsync();
            }
        }
    }
}
