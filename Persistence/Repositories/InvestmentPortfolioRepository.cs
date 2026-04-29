using ProyectoDeAprendizajeP3.Core.Domain.Entities;
using ProyectoDeAprendizajeP3.Core.Domain.Interfaces;
using ProyectoDeAprendizajeP3.Infrastruture.Persistence.Contexts;

namespace ProyectoDeAprendizajeP3.Infrastruture.Persistence.Repositories
{
    public class InvestmentPortfolioRepository : GenericRepository<InvestmentPortfolio>, IInvestmentPortfolioRepository
    {
        public InvestmentPortfolioRepository(InvestmentContext context) : base(context)
        {
        }
    }
}
