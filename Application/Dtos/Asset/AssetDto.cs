using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos.AssetType;

namespace Application.Dtos.Asset
{
    public class AssetDto : BasicDto<int>
    {
        public required string symbol { get; set; }
        public required int AssetTypeId { get; set; }

        public AssetTypeDto? AssetType { get; set; }
    }
}
