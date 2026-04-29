using ProyectoDeAprendizajeP3.Core.Domain.Entities;
using ProyectoDeAprendizajeP3.Core.Domain.Interfaces;
using ProyectoDeAprendizajeP3.Infrastruture.Persistence.Contexts;

namespace ProyectoDeAprendizajeP3.Infrastruture.Persistence.Repositories
{
    public class AssetRepository : GenericRepository<Asset>, IAssetRepository
    {
        public AssetRepository(InvestmentContext context) : base(context)
        {
        }
    }
}
