using ProyectoDeAprendizajeP3.Core.Domain.Entities;

namespace ProyectoDeAprendizajeP3.Core.Domain.Entities
{
    public class AssetHistory
    {
        public required int Id { get; set; }
        public DateTime HistoryValueDate { get; set; }
        public required decimal value { get; set; }
        public required int AssetId { get; set; }
        public Asset? Assets { get; set; }
    }
}
