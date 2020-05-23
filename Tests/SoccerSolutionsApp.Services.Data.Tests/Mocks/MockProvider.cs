using Microsoft.AspNetCore.Identity;
using Moq;
using SoccerSolutionsApp.Data.Models;

namespace SoccerSolutionsApp.Services.Data.Tests.Mocks
{
    public static class MockProvider
    {
        public static UserManager<ApplicationUser> UserManager()
        {

            // TO MOCK REST OF THE PARAMETERS PASSED TO CONSTRUCTOR
            // !!!!!!!!!!

            var userManagerMock = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(),
                null,
                null,
                null,
                null,
                null,
                null,
                null);



            return userManagerMock.Object;
        }
    }
}
