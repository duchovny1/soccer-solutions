using System.Threading.Tasks;

namespace SoccerSolutionsApp.Services.Data.Seasons
{
    public interface ISeasonsService
    {
        Task CreateAsync(ImportSeasonsApi models);
    }
}
