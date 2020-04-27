using Microsoft.EntityFrameworkCore;
using SoccerSolutionsApp.Data.Common.Repositories;
using SoccerSolutionsApp.Data.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerSolutionsApp.Services.Data.Seasons
{
    public class SeasonsService : ISeasonsService
    {
        private readonly IDeletableEntityRepository<Season> seasonsRepo;

        public SeasonsService(IDeletableEntityRepository<Season> seasonsRepo)
        {
            this.seasonsRepo = seasonsRepo;
        }

        public int Create(ImportSeasonsApi models)
        {

            int totalSeasonsAdded = 0;

            foreach (var model in models.Api.Seasons)
            {
                string year = model;
                bool isExists = this.seasonsRepo.All().Any(x => x.StartYear == year);

                if (!isExists)
                {
                    Season season = new Season()
                    {
                        StartYear = year,
                    };

                    this.seasonsRepo.Add(season);
                    totalSeasonsAdded++;
                }
            }

            this.seasonsRepo.SaveChanges();
            return totalSeasonsAdded;
        }
    }
}
