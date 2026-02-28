using Microsoft.Extensions.Caching.Memory;
using NWARE.DataAccess.Interface;
using NWARE.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NWARE.DataAccess
{
    public class CityRepository : ICityRepository
    {
        private readonly IMemoryCache _cache;
        private const string CacheKey = "CityListCache";

        public CityRepository(IMemoryCache cache)
        {
            _cache = cache;
        }
        public async Task<List<CityResponseModel>> InitCitiesListCache()
        {
            if (!_cache.TryGetValue(CacheKey, out List<CityResponseModel> cityList))
            {
                using FileStream fs = File.OpenRead("DataSource/current.city.list.json");
             
                var cities = await JsonSerializer.DeserializeAsync<List<CityModel>>(fs);

                cityList = cities.Select(c => new CityResponseModel
                {
                    CityName = c.name,
                    Country = c.country,
                    Population = c.population
                }).ToList();

                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30), // Cache for 30 minutes
                    SlidingExpiration = TimeSpan.FromMinutes(10) // Reset expiration if accessed within 10 minutes
                };

                _cache.Set(CacheKey, cityList, cacheOptions);
            }

            return cityList;
        }
        public async Task<List<CityResponseModel>> GetCities()
        {
            if(!_cache.TryGetValue(CacheKey, out List<CityResponseModel> cityList))
            {
                cityList = InitCitiesListCache().Result;
            }

            return await Task.FromResult(cityList);
        }

        public Task<List<CityResponseModel>> GetCitiesByName(string cityName)
        {
            if (!_cache.TryGetValue(CacheKey, out List<CityResponseModel> cityList))
            {
                cityList = InitCitiesListCache().Result;
            }

            cityList = cityList.Where(c => c.CityName.Contains(cityName, StringComparison.OrdinalIgnoreCase)).ToList();
            cityList = cityList.GetRange(0, Math.Min(10, cityList.Count));

            return Task.FromResult(cityList);
        }
    }
}
