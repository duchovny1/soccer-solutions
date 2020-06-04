namespace SoccerSolutionsApp.Services.Data.TeamsServices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SoccerSolutionsApp.Services.Data.Teams;
    using SoccerSolutionsApp.Web.ViewModels.Fixtures;
    using SoccerSolutionsApp.Web.ViewModels.Teams;

    public interface ITeamsService
    {
        public Task<IEnumerable<T>> GetAll<T>();

        int Create(ImportTeamsApi models, int leagueId);

        Task<T> GetTeamByIdAsync<T>(int id);


        //public void Create(T entity);
    }
}
