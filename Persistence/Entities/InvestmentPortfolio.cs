using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Common;

namespace Persistence.Entities
{
    public class InvestmentPortfolio : BasicEntity<int>
    {
        public required int UserId { get; set; }

        public User? user { get; set; }
        public ICollection<InvestmentAsset>? investmentAssets { get; set; }
    }
}
