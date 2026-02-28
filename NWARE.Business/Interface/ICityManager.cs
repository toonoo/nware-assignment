using NWARE.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NWARE.Business.Interface
{
    public interface ICityManager
    {
        Task<List<CityResponseModel>> GetCities();
        Task<List<CityResponseModel>> GetCitiesByName(string cityName);
    }
}
