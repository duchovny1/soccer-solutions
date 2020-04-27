namespace SoccerSolutionsApp.Services.Data.Leagues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SoccerSolutionsApp.Data.Common.Repositories;
    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Services.Mapping;

    public class LeaguesService : ILeaguesService
    {

        private readonly IDeletableEntityRepository<League> leagueRepo;
        private readonly IDeletableEntityRepository<Country> countryRepo;
        private readonly IDeletableEntityRepository<Season> seasonRepo;
        private readonly IDeletableEntityRepository<Fixture> fixturesRepo;

        public LeaguesService(
            IDeletableEntityRepository<League> leagueRepo,
            IDeletableEntityRepository<Country> countryRepo,
            IDeletableEntityRepository<Season> seasonRepo,
            IDeletableEntityRepository<Fixture> fixturesRepo)
        {
            this.leagueRepo = leagueRepo;
            this.countryRepo = countryRepo;
            this.seasonRepo = seasonRepo;
            this.fixturesRepo = fixturesRepo;
        }

        public int Create(ImportLeaguesApi models)
        {
            int totalLeagues = 0;
            foreach (var model in models.Api.Leagues)
            {
                if (model.Season != "2018" && model.Season != "2019")
                {
                    continue;
                }

                bool isExist = this.leagueRepo.All().Any(x => x.Name == model.Name
                                    && x.Season.StartYear == model.Season);
                var country = this.countryRepo.All().FirstOrDefault(x => x.Name.ToLower() == model.Country.ToLower());
                var season = this.seasonRepo.All().FirstOrDefault(x => x.StartYear == model.Season);

                DateTime seasonStart;
                DateTime seasonEnd;

                bool seasonStartParse = DateTime.TryParse(model.SeasonStart, out seasonStart);
                if (!isExist && country != null && season != null)
                {
                    League league = new League()
                    {
                        Id = model.LeagueId,
                        Name = model.Name,
                        Type = model.Type,
                        Country = country,
                        Season = season,
                        SeasonStart = DateTime.TryParse(model.SeasonStart, out seasonStart) ? seasonStart : (DateTime?)null,
                        SeasonEnd = DateTime.TryParse(model.SeasonEnd, out seasonEnd) ? seasonStart : (DateTime?)null,
                        Logo = model.Logo,
                    };

                    this.leagueRepo.Add(league);
                    totalLeagues++;
                }
            }

            this.leagueRepo.SaveChanges();
            return totalLeagues;

        }

        public IEnumerable<T> GetAll<T>(int? take = null, int skip = 0)
        {
            IQueryable<League> leagues = this.leagueRepo.All()
                         .Where(x => x.Season.StartYear == "2019")
                         .Skip(skip);

            if (take.HasValue)
            {
                leagues = leagues.Take((int)take);
            }

            return leagues.To<T>().ToList();
        }

        public async Task<int> CountAsync()
            => await this.leagueRepo.All()
                  .Where(x => x.Season.StartYear == "2019").CountAsync();

        public IEnumerable<int> GetAllLeaguesId()
        {
            var leaguesIds = this.leagueRepo.All().Select(x => x.Id).ToArray();

            return leaguesIds;
        }

    }
}
