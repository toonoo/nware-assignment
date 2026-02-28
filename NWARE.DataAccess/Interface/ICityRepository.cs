using NWARE.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NWARE.DataAccess.Interface
{
    public interface ICityRepository
    {
        Task<List<CityResponseModel>> GetCities();
        Task<List<CityResponseModel>> GetCitiesByName(string cityName);
    }
}
