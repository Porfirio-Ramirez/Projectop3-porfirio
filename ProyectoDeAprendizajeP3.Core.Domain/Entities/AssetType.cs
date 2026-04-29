using ProyectoDeAprendizajeP3.Core.Domain.Common;


namespace ProyectoDeAprendizajeP3.Core.Domain.Entities
{
     public class AssetType : BasicEntity<int>
    {
        public ICollection<Asset>? Assets { get; set; }
    }
}
