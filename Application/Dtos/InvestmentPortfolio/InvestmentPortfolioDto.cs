using ProyectoDeAprendizajeP3.Core.Application.Dtos.User;

namespace ProyectoDeAprendizajeP3.Core.Application.Dtos.InvestmentPortfolio
{
    public class InvestmentPortfolioDto : BasicDto<int>
    {
        public required int UserId { get; set; }
        public  UserDto? user { get; set; }
        
    }
}
