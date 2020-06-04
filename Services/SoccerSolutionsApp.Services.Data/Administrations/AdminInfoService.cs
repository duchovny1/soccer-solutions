namespace SoccerSolutionsApp.Services.Data.Administrations
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SoccerSolutionsApp.Data.Common.Repositories;
    using SoccerSolutionsApp.Data.Models;

    public class AdminInfoService : IAdminInfoService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<Prediction> predictionRepository;

        public AdminInfoService(
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IDeletableEntityRepository<Prediction> predictionRepository)
        {
            this.usersRepository = usersRepository;
            this.predictionRepository = predictionRepository;
        }

        public async Task<int> ActiveUsersAsync()
                => await this.predictionRepository.All().Where(x => x.CreatedOn <= DateTime.Today
                 && x.CreatedOn >= DateTime.Now.AddDays(-30)).Select(x => x.User).Distinct().CountAsync();

        public async Task<int> CountUsersAsync()
            => await this.usersRepository.All().CountAsync();
    }
}
