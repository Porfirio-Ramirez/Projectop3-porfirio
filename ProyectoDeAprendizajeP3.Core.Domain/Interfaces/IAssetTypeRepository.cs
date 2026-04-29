namespace ProyectoDeAprendizajeP3.Core.Domain.Interfaces
{
    public interface IAssetTypeRepository
    {
        Task<AssetType> AddAsync(AssetType assetType);
        Task DeleteAsync(int id);
        Task<List<AssetType>> GetAllList();
        IQueryable<AssetType> GetAllQuery();
        Task<AssetType?> GetId(int id);
        Task<AssetType?> UpdateAsync(int id, AssetType assetType);
    }
}