namespace SoccerSolutionsApp.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using MyTested.AspNetCore.Mvc;
    using SoccerSolutionsApp.Data;
    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Services.Data.Tests.Mocks;
    using SoccerSolutionsApp.Services.Mapping;
    using SoccerSolutionsApp.Web;
    using SoccerSolutionsApp.Web.ViewModels;

    public class TestStartup : Startup
    {
        private readonly IWebHostEnvironment hostingEnvironment;

        public TestStartup(
            IWebHostEnvironment hostingEnvironment,
            IConfiguration configuration)
            : base(configuration)
        {
            this.hostingEnvironment = hostingEnvironment;
        }


        public void ConfigureTestServices(IServiceCollection services)
        {
            this.ConfigureServices(services);

            services.ReplaceSingleton<UserManager<ApplicationUser>>(sp =>
                  MockProvider.UserManager());

        }

    }

}

