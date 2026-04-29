

using ProyectoDeAprendizajeP3.Core.Application.ViewModels.AssetHistory;
using ProyectoDeAprendizajeP3.Core.Application.ViewModels.AssetType;

namespace ProyectoDeAprendizajeP3.Core.Application.ViewModels.Asset
{
    public class AssetForPortfolioViewModel : BasicViewModel<int>
    {
        public required string Symbol { get; set; }
        public required int AssetTypeId { get; set; }
        public AssetTypeViewModel? AssetType { get; set; }
        public AssetHistoryViewModel? CurrentAssetHistory { get; set; }
        public decimal? CurrentValue { get; set; } = 0;
    }
}
