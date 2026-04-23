using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.AssetType
{
    public class AssetTypeDto: BasicDto<int>
    {
        public int AssetQuantity { get; set; }
    }
}
