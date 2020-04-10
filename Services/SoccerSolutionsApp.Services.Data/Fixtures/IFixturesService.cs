using System.Threading.Tasks;

namespace SoccerSolutionsApp.Services.Data.Fixtures
{
    public interface IFixturesService
    {
        Task CreateAsync(ImportFixturesApi model);
    }
}
