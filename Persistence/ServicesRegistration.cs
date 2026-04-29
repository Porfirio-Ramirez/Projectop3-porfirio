using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProyectoDeAprendizajeP3.Core.Domain.Interfaces;
using ProyectoDeAprendizajeP3.Infrastruture.Persistence.Contexts;
using ProyectoDeAprendizajeP3.Infrastruture.Persistence.Repositories;

namespace ProyectoDeAprendizajeP3.Infrastruture.Persistence
{
    public static class ServicesRegistration
    {
        public static void AddPersistenceLayerIoc(this IServiceCollection services, IConfiguration config)
        {
            #region Context
            if (config.GetValue<bool>("UseInMemoryDatabase"))
            {

            }
            else
            {
                var connectionstring = config.GetConnectionString("DefaultConnection");
                services.AddDbContext<InvestmentContext>(opt =>
                opt.UseSqlServer(connectionstring,
                m => m.MigrationsAssembly(typeof(InvestmentContext).Assembly.FullName)),
                ServiceLifetime.Transient);

            }
            #endregion

            #region Repositories IOC
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IAssetHistoryRepository, AssetHistoryRepository>();
            services.AddTransient<IAssetRepository, AssetRepository>();
            services.AddTransient<IAssetTypeRepository, AssetTypeRepository>();
            services.AddTransient<IInvestmentAssetRepository, InvestmentAssetRepository>();
            services.AddTransient<IInvestmentPortfolioRepository, InvestmentPortfolioRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            #endregion
        }
    }
}
