using Castle.Core.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SoccerSolutionsApp.Data.Models;
using System;
using System.Collections.Generic;

namespace SoccerSolutionsApp.Services.Data.Tests.Mocks
{
    public class UserManagerMock : UserManager<ApplicationUser>
    {
        public UserManagerMock(
            IUserStore<ApplicationUser> store,
            IOptions<IdentityOptions> options,
            IPasswordHasher<ApplicationUser> passwordHasher,
            IEnumerable<IUserValidator<ApplicationUser>> userValidators,
            IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<ApplicationUser>> logger)
            : base(
                store,
                options,
                passwordHasher,
                userValidators,
                passwordValidators,
                keyNormalizer,
                errors,
                services,
                logger)
        {
        }
    }
}
