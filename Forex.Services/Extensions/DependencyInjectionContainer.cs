using Forex.Data;
using Forex.Services.Contracts;
using Forex.Services.Implementations;
using Forex.Services.Implementations.Commands;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Forex.Services.Extensions
{
    public static class DependencyInjectionContainer
    {
        public static IServiceCollection RegisterAppServices(this IServiceCollection services)
        {
            services.AddSingleton<IMarketDataProvider, MarketDataProvider>();
            services.AddSingleton<IDatabaseInitializer, DatabaseInitializer>();
            services.AddSingleton<ICalculationServiceProvider, CalculationServiceProvider>();
            services.AddSingleton<ICommandFactory, CommandFactory>();
            services.AddSingleton<IMemoryCache, MemoryCache>();
            services.AddDbContext<ForexDbContext>(ServiceLifetime.Transient);

            return services;
        }
    }
}
