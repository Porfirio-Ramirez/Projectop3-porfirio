

using ProyectoDeAprendizajeP3.Core.Application.Dtos.AssetHistory;
using ProyectoDeAprendizajeP3.Core.Application.Dtos.AssetType;

namespace ProyectoDeAprendizajeP3.Core.Application.Dtos.Asset
{
    public class AssetForPortfolioDto : BasicDto<int>
    {
        public required string Symbol { get; set; }
        public required int AssetTypeId { get; set; }
        public AssetTypeDto? AssetType { get; set; }
        public AssetHistoryDto? CurrentAssetHistory { get; set; }
        public decimal? CurrentValue { get; set; } = 0;
    }
}
