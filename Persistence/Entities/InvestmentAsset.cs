using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Entities
{
    public class InvestmentAsset
    {
        public required int AssetId { get; set; } // FK
        public Asset? asset { get; set; } // navigation 
        public required int InvestmentPortfolioId { get; set; } //FK
        public InvestmentPortfolio? investmentPortfolio { get; set; } // navigation 
        public DateTime associationdate { get; set; } = DateTime.UtcNow;
    }
}
