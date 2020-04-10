namespace SoccerSolutionsApp.Services.Data.Countries
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICountriesService
    {
        public IEnumerable<T> GetAll<T>();

        Task CreateAsync(ImportCountriesApi models);
    }
}
