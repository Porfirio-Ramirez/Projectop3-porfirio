using ProyectoDeAprendizajeP3.Core.Application.Dtos.AssetType;

namespace ProyectoDeAprendizajeP3.Core.Application.Interfaces
{
    public interface IAssetTypeService
    {
        Task<bool> AddAsync(AssetTypeDto assetTypeDto);
        Task<bool> DeleteAsync(int id);
        Task<List<AssetTypeDto>> GetAll();
        Task<List<AssetTypeDto>> GetAllWithInclude();
        Task<AssetTypeDto?> GetById(int id);
        Task<bool> UpdateAsync(AssetTypeDto assetTypeDto);
    }
}