using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Common;

namespace Persistence.Entities
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
