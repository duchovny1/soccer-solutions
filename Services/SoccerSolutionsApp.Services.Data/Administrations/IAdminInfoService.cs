using System.Threading.Tasks;

namespace SoccerSolutionsApp.Services.Data.Administrations
{
    public interface IAdminInfoService
    {
        Task<int> CountUsersAsync();

        Task<int> ActiveUsersAsync();
    }
}
