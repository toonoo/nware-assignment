using NWARE.Common;
using NWARE.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NWARE.Business.Interface
{
    public interface ICityManager
    {
        Task<ServiceResult<List<CityResponseModel>>> GetCities();
        Task<ServiceResult<List<CityResponseModel>>> GetCitiesByName(string cityName);
    }
}
