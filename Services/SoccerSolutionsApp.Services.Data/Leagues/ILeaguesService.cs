namespace SoccerSolutionsApp.Services.Data.Leagues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface ILeaguesService
    {
        int Create(ImportLeaguesApi models);

        IEnumerable<int> GetAllLeaguesId();

        Task<int> CountAsync();

        IEnumerable<T> GetAll<T>(int? take = null, int skip = 0);

       

    }
}
