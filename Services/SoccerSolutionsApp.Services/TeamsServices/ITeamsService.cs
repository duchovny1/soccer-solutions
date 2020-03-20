namespace SoccerSolutionsApp.Services.TeamsServices
{
    using System.Collections.Generic;

    public interface ITeamsService
    {
        public IEnumerable<T> GetAll<T>();
    }
}
