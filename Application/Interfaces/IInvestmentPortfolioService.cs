using ProyectoDeAprendizajeP3.Core.Application.Dtos.InvestmentPortfolio;

namespace ProyectoDeAprendizajeP3.Core.Application.Interfaces
{
    public interface IInvestmentPortfolioService
    {

        Task<bool> AddAsync(InvestmentPortfolioDto dto);

        Task<bool> UpdateAsync(InvestmentPortfolioDto dto);
        Task<bool> DeleteAsync(int id);
        Task<List<InvestmentPortfolioDto>> GetAll();
        Task<List<InvestmentPortfolioDto>> GetAllWithInclude();
        Task<InvestmentPortfolioDto?> GetById(int id);
    }
}
