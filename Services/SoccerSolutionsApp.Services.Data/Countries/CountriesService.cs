﻿namespace SoccerSolutionsApp.Services.Data.Countries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using SoccerSolutionsApp.Data.Common.Repositories;
    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Services.Mapping;
    using SoccerSolutionsApp.Web.ViewModels.Competitions;

    public class CountriesService : ICountriesService
    {
        private readonly IDeletableEntityRepository<Country> countriesRepository;
        private readonly IDeletableEntityRepository<League> leaguesRepository;

        public CountriesService(
            IDeletableEntityRepository<Country> countriesRepository,
            IDeletableEntityRepository<League> leaguesRepository)
        {
            this.countriesRepository = countriesRepository;
            this.leaguesRepository = leaguesRepository;
        }

        public int Create(ImportCountriesApi models)
        {
            int totalCountriesAdd = 0;

            foreach (var model in models.Api.Countries)
            {
                bool doesTheCountryExists = this.countriesRepository.All().Any(x => x.Name == model.Country);

                if (!doesTheCountryExists && model.Code != null && model.Country != null)
                {
                    Country country = new Country
                    {
                        Name = model.Country,
                        Code = model.Code,
                        Flag = model.Flag,
                    };

                    this.countriesRepository.Add(country);
                    totalCountriesAdd++;
                }
            }

            this.countriesRepository.SaveChanges();
            return totalCountriesAdd;
        }

        public IEnumerable<T> GetAll<T>()
        {
            var countries = this.countriesRepository.All().OrderBy(x => x.Name);

            return countries.To<T>().ToList();
        }

        
        public IEnumerable<CountriesListingViewModel> GetCountriesWithLeagues()
         => this.countriesRepository.All()
            .To<CountriesListingViewModel>()
            .ToList();

        public async Task<int> GetCountryIdByName(string countryName)
          => await this.countriesRepository.All().Where(x => x.Name == countryName)
            .Select(x => x.Id).FirstOrDefaultAsync();
    }
}
