

using ProyectoDeAprendizajeP3.Core.Application.ViewModels.Asset;

namespace ProyectoDeAprendizajeP3.Core.Application.ViewModels.InvestmentAssets
{
    public class InvestmentAssetsViewModel
    {
        public required int Id { get; set; }
        public required int AssetId { get; set; }
        public AssetViewModel? Asset { get; set; }
        public required int InvestmentPortfolioId { get; set; }
        public InvestmentAssetsViewModel? InvestmentPortfolio { get; set; }
        public DateTime AssociationDate { get; set; } = DateTime.UtcNow;
    }
}
