namespace SoccerSolutionsApp.Services.TeamsServices
{
    using System.Collections.Generic;
    using System.Linq;

    using SoccerSolutionsApp.Data.Common.Repositories;
    using SoccerSolutionsApp.Data.Models;

    using SoccerSolutionsApp.Services.Mapping;

    public class TeamsService : ITeamsService
    {
        private readonly IDeletableEntityRepository<Team> teamRepository;

        public TeamsService(IDeletableEntityRepository<Team> teamRepository)
        {
            this.teamRepository = teamRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            IQueryable<Team> query = this.teamRepository.All();
            return query.To<T>()
                .ToList();
            //return query.Tйo<T>.ToList();
        }
    }
}
