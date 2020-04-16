namespace SoccerSolutionsApp.Services.Data.Leagues
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface ILeaguesService
    {
        Task CreateAsync(ImportLeaguesApi models);

        IEnumerable<int> GetAllLeaguesId();

        IEnumerable<T> GetAll<T>();
    }
}
