using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProyectoDeAprendizajeP3.Infrastruture.Persistence.Contexts;

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
            services.AddTransient(typeof(IGenericRepository<>))
            #endregion
        }
    }
}
