namespace SoccerSolutionsApp.Services.Data.Fixtures
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SoccerSolutionsApp.Web.ViewModels.Fixtures;

    public interface IFixturesService
    {
        Task CreateAsync(ImportFixturesApi model);

        IEnumerable<FixtureViewModel> GetFixturesByDate(FixturesByDateInputModel model);
    }
}
