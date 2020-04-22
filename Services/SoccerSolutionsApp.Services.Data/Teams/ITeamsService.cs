namespace SoccerSolutionsApp.Services.Data.TeamsServices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SoccerSolutionsApp.Services.Data.Teams;
    using SoccerSolutionsApp.Web.ViewModels.Fixtures;
    using SoccerSolutionsApp.Web.ViewModels.Teams;

    public interface ITeamsService
    {
        public IEnumerable<T> GetAll<T>();

        void CreateAsync(ImportTeamsApi models, int leagueId);

        Task<T> GetTeamByIdAsync<T>(int id);


        //public void Create(T entity);
    }
}
