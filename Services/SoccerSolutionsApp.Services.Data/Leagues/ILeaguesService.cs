namespace SoccerSolutionsApp.Services.Data.Leagues
{
    using System.Threading.Tasks;

    public interface ILeaguesService
    {
        Task CreateAsync(ImportLeaguesApi models);
    }
}
