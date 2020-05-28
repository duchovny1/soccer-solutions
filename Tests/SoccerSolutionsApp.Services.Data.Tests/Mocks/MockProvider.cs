using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;
using SoccerSolutionsApp.Data.Models;
using System;
using System.Collections.Generic;

namespace SoccerSolutionsApp.Services.Data.Tests.Mocks
{
    public static class MockProvider
    {
        public static UserManager<ApplicationUser> UserManager()
        {

            // TO MOCK REST OF THE PARAMETERS PASSED TO CONSTRUCTOR
            // !!!!!!!!!!

            var userValidatorMock = new Mock<IUserValidator<ApplicationUser>>();
            var userValidators = new List<IUserValidator<ApplicationUser>>
            {
                userValidatorMock.Object,
            };

            var passwordValidatorMock = new Mock<IPasswordValidator<ApplicationUser>>();
            var passwordValidators = new List<IPasswordValidator<ApplicationUser>>
            {
                passwordValidatorMock.Object,
            };

            var userManagerMock = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(),
                Mock.Of<IOptions<IdentityOptions>>(),
                Mock.Of<IPasswordHasher<ApplicationUser>>(),
                userValidators,
                passwordValidators,
                Mock.Of<ILookupNormalizer>(),
                Mock.Of<IdentityErrorDescriber>(),
                Mock.Of<IServiceProvider>(),
                null);

            return userManagerMock.Object;
        }
    }
}
