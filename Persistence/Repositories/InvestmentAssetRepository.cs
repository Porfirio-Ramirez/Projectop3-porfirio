using ProyectoDeAprendizajeP3.Core.Domain.Entities;
using ProyectoDeAprendizajeP3.Core.Domain.Interfaces;
using ProyectoDeAprendizajeP3.Infrastruture.Persistence.Contexts;

namespace ProyectoDeAprendizajeP3.Infrastruture.Persistence.Repositories
{
    public class InvestmentAssetRepository : GenericRepository<InvestmentAsset>, IInvestmentAssetRepository
    {
        public InvestmentAssetRepository(InvestmentContext context) : base(context)
        {
        }
    }
}
