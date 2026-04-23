using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Common;

namespace Persistence.Entities
{
     public class AssetType : BasicEntity<int>
    {
        public ICollection<Asset>? Assets { get; set; }
    }
}
