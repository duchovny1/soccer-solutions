namespace SoccerSolutionsApp.Services.Data.Countries
{
    using SoccerSolutionsApp.Web.ViewModels.Competitions;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICountriesService
    {
        IEnumerable<T> GetAll<T>();

        int Create(ImportCountriesApi models);

        Task<int> GetCountryIdByName(string coutryName);

        IEnumerable<CountriesListingViewModel> GetCountriesWithLeagues();
    }
}
