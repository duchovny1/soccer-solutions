namespace SoccerSolutionsApp.Services.Data.Leagues
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILeaguesService
    {
        Task CreateAsync(ImportLeaguesApi models);

        IEnumerable<int> GetAllLeaguesId();
    }
}
