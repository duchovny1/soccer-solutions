﻿namespace SoccerSolutionsApp.Services.Data.Users
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SoccerSolutionsApp.Data.Models;

    public interface IUsersService
    {
        Task FollowUserAsync(string userToFollow, string userFollowing);

        Task UnfollowUserAsync(string userToFollow, string userFollowing);

        Task<IEnumerable<string>> GetCurrentUserFollowings(string userId);
    }
}
