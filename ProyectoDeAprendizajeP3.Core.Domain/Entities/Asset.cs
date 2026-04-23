using ProyectoDeAprendizajeP3.Core.Domain.Common;


namespace ProyectoDeAprendizajeP3.Core.Domain.Entities
{
     public class Asset : BasicEntity<int>
    {
        public required string symbol { get; set; }
        public required int AssetTypeId { get; set; }

        public AssetType? AssetType { get; set; }
        public ICollection<AssetHistory>? AssetHistories { get; set; }
        public ICollection<InvestmentAsset>? investmentAssets { get; set; }

    }
}
