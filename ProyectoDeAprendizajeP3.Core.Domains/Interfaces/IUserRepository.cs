using ProyectoDeAprendizajeP3.Core.Domain.Entities;

namespace ProyectoDeAprendizajeP3.Core.Domain.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> LoginAsync(string userName, string password);
    }
}
