using ProyectoDeAprendizajeP3.Core.Domain.Entities;

namespace ProyectoDeAprendizajeP3.Core.Domain.Interfaces
{
    public interface IGenericRepository<Entity>
        where Entity : class
    {
        Task<Entity?> AddAsync(Entity entity);
        Task<List<Entity>?> AddRangeAsync(List<Entity> entities);
        Task<Entity?> UpdateAsync(int id, Entity entity);
        Task DeleteAsync(int id);
        Task<List<Entity>> GetAllList();
        IQueryable<Entity> GetAllQuery();
        Task<Entity?> GetById(int id);
       
        Task<List<Entity>> GetAllListWithInclude(List<string> properties);
        IQueryable<Entity> GetAllQueryWithInclude(List<string> properties);
    }
}
