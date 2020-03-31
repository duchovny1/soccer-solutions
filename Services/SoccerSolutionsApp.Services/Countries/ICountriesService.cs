namespace SoccerSolutionsApp.Services.Countries
{
    using System.Collections.Generic;

    public interface ICountriesService
    {
        public IEnumerable<T> GetAll<T>();

    }
}
