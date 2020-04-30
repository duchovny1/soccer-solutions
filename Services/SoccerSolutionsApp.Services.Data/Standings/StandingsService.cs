namespace SoccerSolutionsApp.Services.Data.Standings
{
    using System;
    using System.Collections.Generic;
    using SoccerSolutionsApp.Data.Common.Repositories;
    using SoccerSolutionsApp.Data.Models;

    public class StandingsService : IStandingsService
    {
        private readonly IDeletableEntityRepository<Standing> standingsRepository;

        public StandingsService(IDeletableEntityRepository<Standing> standingsRepository)
        {
            this.standingsRepository = standingsRepository;
        }

        public int Create(ImportStandingsApi models)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetStanding<T>(string leagueName)
        {
            throw new NotImplementedException();
        }
    }
}
