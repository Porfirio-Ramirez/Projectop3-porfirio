

using ProyectoDeAprendizajeP3.Core.Application.ViewModels.Asset;

namespace ProyectoDeAprendizajeP3.Core.Application.ViewModels.AssetHistory
{
    public class AssetHistoryViewModel
    {
        public required int Id { get; set; }
        public DateTime HistoryValueDate { get; set; }
        public required decimal Value { get; set; }
        public required int AssetId { get; set; }
        public AssetViewModel? Asset { get; set; }
    }
}
