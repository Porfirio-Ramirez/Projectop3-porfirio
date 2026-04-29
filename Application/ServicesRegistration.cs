using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProyectoDeAprendizajeP3.Core.Application.Interfaces;
using ProyectoDeAprendizajeP3.Core.Application.Services;
using ProyectoDeAprendizajeP3.Core.Domain.Interfaces;


namespace ProyectoDeAprendizajeP3.Infrastruture.Persistence
{
    public static class ServicesRegistration
    {
        public static void AddAplicationLayerIoc(this IServiceCollection services)
        {
          

            #region Services IOC
            services.AddTransient<IAssetService, AssetService>();
            services.AddTransient<IAssetHistoryService, AssetHistoryService>();
            services.AddTransient<IAssetTypeService, AssetTypeService>();
            services.AddTransient<IInvestmentAssetService, InvestmentAssetService>();
            services.AddTransient<IInvestmentPortfolioService, InvestmentPortfolioService>();
            services.AddTransient<IUserService, UserService>();
            #endregion
        }
    }
}
