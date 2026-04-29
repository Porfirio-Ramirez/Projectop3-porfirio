using ProyectoDeAprendizajeP3.Core.Domain.Common;

namespace ProyectoDeAprendizajeP3.Core.Domain.Entities
{
    public class InvestmentPortfolio : BasicEntity<int>
    {
        public required int UserId { get; set; }

        public User? user { get; set; }
        public ICollection<InvestmentAsset>? investmentAssets { get; set; }
    }
}
