namespace SoccerSolutionsApp.Services.Data.Leagues
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface ILeaguesService
    {
        Task CreateAsync(ImportLeaguesApi models);

        IEnumerable<int> GetAllLeaguesId();

        Task<int> CountAsync();

        IEnumerable<T> GetAll<T>(int take, int skip = 0);
    }
}
