using CatMash.Common.Config;
using CatMash.Domain.Configuration;
using CatMash.Infra.Data.Configuration;
using CatMash.Infra.Data.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CatMash.Infra.Data
{
    public static class IocInstaller
    {
        public static IServiceCollection AddInfraDataDependencies(this IServiceCollection services, IConfiguration config)
        {
            // Config
            services.AddSingleton<IConfigRepo<CatMashConfig>, CatMashConfigRepo>();
            services.Configure<CatMashConfigSection>(options => config.GetSection("CatMash").Bind(options));
            services.AddSingleton<IConfigSectionValidator<CatMashConfigSection>, ConfigSectionValidator<CatMashConfigSection>>();

            services.AddSingleton<ICatsRepository, CatsRepository>();
            services.AddSingleton<IFileParser, FileParser>();

            return services;
        }
    }
}
