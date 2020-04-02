namespace SoccerSolutionsApp.Services.Data.TeamsServices
{
    using System.Collections.Generic;

    public interface ITeamsService
    {
        public IEnumerable<T> GetAll<T>();

        //public void Create(T entity);
    }
}
