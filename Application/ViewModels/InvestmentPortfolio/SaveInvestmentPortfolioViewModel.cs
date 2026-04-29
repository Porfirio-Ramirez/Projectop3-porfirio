

using System.ComponentModel.DataAnnotations;

namespace ProyectoDeAprendizajeP3.Core.Application.ViewModels.InvestmentPortfolio
{
    public class SaveInvestmentPortfolioViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You must enter the name of investment portfolio")]
        public required string Name { get; set; }
        public string? Description { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "You must enter the valid user of investment portfolio")]
        public int UserId { get; set; }
    }
}
