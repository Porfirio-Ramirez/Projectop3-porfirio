using ProyectoDeAprendizajeP3.Core.Application.Dtos.Asset;

namespace ProyectoDeAprendizajeP3.Core.Application.Dtos.AssetHistory
{
    public class AssetHistoryDto
    {
        public required int Id { get; set; }
        public DateTime HistoryValueDate { get; set; }
        public required decimal Value { get; set; }
        public required int AssetId { get; set; }
        public AssetDto? Asset { get; set; }
    }
}
