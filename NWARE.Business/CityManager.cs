using NWARE.Business.Interface;
using NWARE.Common;
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

        public async Task<ServiceResult<List<CityResponseModel>>> GetCities()
        {
            var cities = await this._cityRepository.GetCities();
            if (cities == null)
            {
                return ServiceResult<List<CityResponseModel>>.Fail("City repository is not available.");
            }
            return ServiceResult<List<CityResponseModel>>.SuccessList(cities);
        }

        public async Task<ServiceResult<List<CityResponseModel>>> GetCitiesByName(string cityName)
        {
            ServiceResult<List<CityResponseModel>> result;
            try
            {
                var city = await this._cityRepository.GetCitiesByName(cityName);
                if (city == null)
                {
                    return ServiceResult<List<CityResponseModel>>.Fail($"No cities found with the name '{cityName}'.");
                }
                result = ServiceResult<List<CityResponseModel>>.SuccessList(city);
            }
            catch (System.Exception ex)
            {
                return ServiceResult<List<CityResponseModel>>.Fail($"An error occurred while retrieving cities: {ex.Message}");
            }

            return result;
        }
    }
}
