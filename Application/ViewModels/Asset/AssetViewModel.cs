using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoDeAprendizajeP3.Core.Application.ViewModels.AssetHistory;
using ProyectoDeAprendizajeP3.Core.Application.ViewModels.AssetType;

namespace ProyectoDeAprendizajeP3.Core.Application.ViewModels.Asset
{
    public class AssetViewModel : BasicViewModel<int>
    {
        public required string Symbol { get; set; }

        public required int AssetTypeId { get; set; }
        public AssetTypeViewModel? AssetType { get; set; }
        public ICollection<AssetHistoryViewModel>? AssetHistories { get; set; }
    }
}
