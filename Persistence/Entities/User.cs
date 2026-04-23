using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Entities
{
    public class User
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? Phone { get; set; }
        public string? ProfileImage { get; set; }
        public required int Role { get; set; }

        public ICollection<InvestmentPortfolio>? investmentPortfolios { get; set; }
    }
}
