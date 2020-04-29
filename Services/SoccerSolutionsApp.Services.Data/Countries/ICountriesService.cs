namespace SoccerSolutionsApp.Services.Data.Countries
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICountriesService
    {
        public IEnumerable<T> GetAll<T>();

        int Create(ImportCountriesApi models);

        Task<int> GetCountryIdByName(string coutryName);
    }
}
