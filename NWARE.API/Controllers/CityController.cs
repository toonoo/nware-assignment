using Microsoft.AspNetCore.Mvc;
using NWARE.Business.Interface;
using NWARE.Common;
using NWARE.Domain;
using NWARE.API.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NWARE.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class CityController : Controller
    {
        private readonly ICityManager _cityManager;
        public CityController(ICityManager cityManager) 
        { 
            this._cityManager = cityManager;
        }

        [HttpGet("GetCities")]
        public async Task<ServiceResult<List<CityResponseModel>>> GetCities()
        {
           return await this._cityManager.GetCities();
        }

        [HttpGet("GetCitiesByName")]
        public async Task<ServiceResult<List<CityResponseModel>>> GetCitiesByName(string cityName)
        {
            return await this._cityManager.GetCitiesByName(cityName);
        }
    }
}
