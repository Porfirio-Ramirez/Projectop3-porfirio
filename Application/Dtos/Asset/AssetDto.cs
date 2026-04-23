using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoDeAprendizajeP3.Core.Application.Dtos.AssetType;

namespace ProyectoDeAprendizajeP3.Core.Application.Dtos.Asset
{
    public class AssetDto : BasicDto<int>
    {
        public required string symbol { get; set; }
        public required int AssetTypeId { get; set; }

        public AssetTypeDto? AssetType { get; set; }
    }
}
