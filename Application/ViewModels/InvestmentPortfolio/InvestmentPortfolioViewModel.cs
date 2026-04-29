

using ProyectoDeAprendizajeP3.Core.Application.ViewModels.User;

namespace ProyectoDeAprendizajeP3.Core.Application.ViewModels.InvestmentPortfolio
{
    public class InvestmentPortfolioViewModel : BasicViewModel<int>
    {
        public required int UserId { get; set; }
        public UserViewModel? User { get; set; }
    }
}
