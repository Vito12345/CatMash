using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CatMash.Business
{
    public static class IocInstaller
    {
        public static IServiceCollection AddBusinessDependencies(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<ICatManager, CatManager>();

            return services;
        }
    }
}
