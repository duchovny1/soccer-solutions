namespace SoccerSolutionsApp.Services.Data.Leagues
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SoccerSolutionsApp.Data.Common.Repositories;
    using SoccerSolutionsApp.Data.Models;

    public class LeaguesService : ILeaguesService
    {
        private readonly IDeletableEntityRepository<League> leagueRepo;
        private readonly IDeletableEntityRepository<Country> countryRepo;
        private readonly IDeletableEntityRepository<Season> seasonRepo;

        public LeaguesService(
            IDeletableEntityRepository<League> leagueRepo,
            IDeletableEntityRepository<Country> countryRepo,
            IDeletableEntityRepository<Season> seasonRepo)
        {
            this.leagueRepo = leagueRepo;
            this.countryRepo = countryRepo;
            this.seasonRepo = seasonRepo;
        }

        public async Task CreateAsync(ImportLeaguesApi models)
        {
            foreach (var model in models.Api.Leagues)
            {
                bool isExist = await this.leagueRepo.All().AnyAsync(x => x.Name == model.Name);
                var country = await this.countryRepo.All().FirstOrDefaultAsync(x => x.Name.ToLower() == model.Country.ToLower());
                var season = await this.seasonRepo.All().FirstOrDefaultAsync(x => x.StartYear == model.Season);

                if (!isExist && country != null && season != null)
                {
                    League league = new League()
                    {
                        Name = model.Name,
                        Type = model.Type,
                        Country = country,
                        Season = season,
                        SeasonStart = DateTime.Parse(model.SeasonStart),
                        SeasonEnd = DateTime.Parse(model.SeasonEnd),
                        Logo = model.Logo,
                    };

                    await this.leagueRepo.AddAsync(league);
                }

                await this.leagueRepo.SaveChangesAsync();
            }
        }
    }
}
