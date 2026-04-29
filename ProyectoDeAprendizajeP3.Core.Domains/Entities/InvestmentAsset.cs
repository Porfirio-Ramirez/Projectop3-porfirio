using ProyectoDeAprendizajeP3.Core.Domain.Entities;

namespace ProyectoDeAprendizajeP3.Core.Domain.Entities
{
    public class InvestmentAsset
    {
        public required int Id { get; set; }
        public required int AssetId { get; set; } // FK
        public Asset? asset { get; set; } // navigation 
        public required int InvestmentPortfolioId { get; set; } //FK
        public InvestmentPortfolio? investmentPortfolio { get; set; } // navigation 
        public DateTime associationdate { get; set; } = DateTime.UtcNow;
    }
}
