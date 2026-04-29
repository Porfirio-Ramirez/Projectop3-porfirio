using ProyectoDeAprendizajeP3.Core.Application.Dtos.Asset;
using ProyectoDeAprendizajeP3.Core.Application.Dtos.InvestmentPortfolio;

namespace ProyectoDeAprendizajeP3.Core.Application.Dtos.InvestmentAsset
{
    public class InvestmentAssetDto
    {
        public required int Id { get; set; }
        public required int AssetId { get; set; }
        public AssetDto? Asset { get; set; }

        public required int InvestmentPortfolioId { get; set; }
        public InvestmentPortfolioDto? InvestmentPortfolio { get; set; }

        public DateTime AssociationDate { get; set; } = DateTime.UtcNow;
    }
}
