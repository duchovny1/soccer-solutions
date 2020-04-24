namespace SoccerSolutionsApp.Services.Data.Users
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SoccerSolutionsApp.Data.Common.Repositories;
    using SoccerSolutionsApp.Data.Models;

    public class UserService : IUsersService
    {
        private readonly IDeletableEntityRepository<Following> followingsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public UserService(
            IDeletableEntityRepository<Following> followingsRepository,
            IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.followingsRepository = followingsRepository;
            this.usersRepository = usersRepository;
        }

        public async Task FollowUserAsync(string userToFollow, string userFollowing)
        {
            if (userToFollow != null & userFollowing != null)
            {
                var ifAlreadyExists = this.followingsRepository.AllWithDeleted()
                    .FirstOrDefault(x => x.UserToFollowId == userToFollow && x.UserFollowingId == userFollowing);

                if (ifAlreadyExists != null)
                {
                    ifAlreadyExists.IsDeleted = false;
                }
                else
                {
                    Following following = new Following()
                    {
                        UserToFollowId = userToFollow,
                        UserFollowingId = userFollowing,
                    };

                    await this.followingsRepository.AddAsync(following);
                }

                await this.followingsRepository.SaveChangesAsync();
            }
        }

        public async Task UnfollowUserAsync(string userToUnfollow, string userUnFollowing)
        {
            if (userToUnfollow != null & userUnFollowing != null)
            {
                Following followingToDelete = this.followingsRepository.All()
                         .FirstOrDefault(x => x.UserFollowingId == userUnFollowing
                         && x.UserToFollowId == userToUnfollow);

                this.followingsRepository.Delete(followingToDelete);

                await this.followingsRepository.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<string>> GetCurrentUserFollowings(string userId)
        {
            // userId is a currentUser . getting people he follow

            var followings = await this.followingsRepository.All()
                .Where(x => x.UserFollowingId == userId)
                .Select(x => x.UserToFollow.UserName)
                .ToListAsync();

            return followings;

        }
    }
}
