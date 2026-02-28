using Microsoft.Extensions.DependencyInjection;
using NWARE.Business.Interface;

namespace NWARE.Business
{
    public class Dependencies
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<ICityManager, CityManager>();
            DataAccess.Dependencies.Register(services);
        }
    }
}
