using ProyectoDeAprendizajeP3.Core.Application.Dtos.InvestmentAsset;


namespace ProyectoDeAprendizajeP3.Core.Application.Interfaces
{
    public interface IInvestmentAssetService
    {
        Task<bool> AddAsync(InvestmentAssetDto dto);
        Task<bool> DeleteAsync(int id);
        Task<List<InvestmentAssetDto>> GetAll();
        Task<List<InvestmentAssetDto>> GetAllWithInclude();
        Task<InvestmentAssetDto?> GetById(int id);
        Task<bool> UpdateAsync(InvestmentAssetDto dto);
        Task<InvestmentAssetDto?> GetByAssetAndPortfolioAsync(int assetId, int portfolioId);
    }
}
