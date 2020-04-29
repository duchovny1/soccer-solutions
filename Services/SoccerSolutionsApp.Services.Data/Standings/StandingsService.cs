namespace SoccerSolutionsApp.Services.Data.Standings
{
    using System;

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
    }
}
