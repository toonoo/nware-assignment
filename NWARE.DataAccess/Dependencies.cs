using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using NWARE.DataAccess.Interface;

namespace NWARE.DataAccess
{
    public class Dependencies
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<ICityRepository, CityRepository>();
        }
    }
}
