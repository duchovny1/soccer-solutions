using System.Collections.Generic;
namespace SoccerSolutionsApp.Services.Countries
{
    public interface ICountriesService
    {
        public IEnumerable<T> GetAll<T>();

    }
}
