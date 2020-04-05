namespace SoccerSolutionsApp.Services.Data.TeamsServices
{
    using SoccerSolutionsApp.Services.Data.Teams;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITeamsService
    {
        public IEnumerable<T> GetAll<T>();

        Task CreateAsync(ImportTeamsApi models, int leagueId);
        //public void Create(T entity);
    }
}
