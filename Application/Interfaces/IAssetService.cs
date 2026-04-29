using ProyectoDeAprendizajeP3.Core.Application.Dtos.Asset;

namespace ProyectoDeAprendizajeP3.Core.Application.Interfaces
{
    public interface IAssetService
    {
        Task<bool> AddAsync(AssetDto dto);
        Task<bool> DeleteAsync(int id);
        Task<List<AssetDto>> GetAll();
        Task<List<AssetDto>> GetAllWithInclude();
        Task<AssetDto?> GetById(int id);
        Task<bool> UpdateAsync(AssetDto dto);
        Task<List<AssetForPortfolioDto>> GetAllAssetsByPortfolioId(int portfolioId, string? assetName = null, int? assetTypeId = null, int? assetOrderBy = null);


    }
}