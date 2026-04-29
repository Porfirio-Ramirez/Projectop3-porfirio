using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoDeAprendizajeP3.Core.Application.Dtos.AssetType
{
    public class AssetTypeDto: BasicDto<int>
    {
        public int AssetQuantity { get; set; }
    }
}
