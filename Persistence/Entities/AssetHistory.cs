using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Entities
{
    public class AssetHistory
    {
        public required int Id { get; set; }
        public DateTime HistoryValueDate { get; set; }
        public required decimal value { get; set; }
        public required int AssetId { get; set; }
        public Asset? Assets { get; set; }
    }
}
