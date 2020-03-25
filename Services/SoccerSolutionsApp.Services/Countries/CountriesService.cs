namespace SoccerSolutionsApp.Services.Countries
{
    using System.Collections.Generic;
    using System.Linq;

    using SoccerSolutionsApp.Data.Common.Repositories;
    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Services.Mapping;

    public class CountriesService : ICountriesService
    {
        private readonly IDeletableEntityRepository<Country> countriesRepository;

        public CountriesService(IDeletableEntityRepository<Country> countriesRepository)
        {
            this.countriesRepository = countriesRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            var countries = this.countriesRepository.All().OrderBy(x => x.Name);

            return countries.To<T>().ToList();
        }
    }
}
