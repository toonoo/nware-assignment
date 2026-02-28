using NWARE.Business.Interface;
using NWARE.DataAccess.Interface;
using NWARE.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NWARE.Business
{
    public class CityManager : ICityManager
    {
        private readonly ICityRepository _cityRepository;
        public CityManager(ICityRepository cityRepository) 
        { 
            this._cityRepository = cityRepository;
        }

        public async Task<List<CityResponseModel>> GetCities()
        {
            return await this._cityRepository.GetCities();
        }

        public async Task<List<CityResponseModel>> GetCitiesByName(string cityName)
        {
            return await this._cityRepository.GetCitiesByName(cityName);
        }
    }
}
