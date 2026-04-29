using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoDeAprendizajeP3.Core.Application.ViewModels.AssetType
{
    public class AssetTypeViewModel : BasicViewModel<int>
    {
        public int? AssetQuantity { get; set; }
    }
}
