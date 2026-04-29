using ProyectoDeAprendizajeP3.Core.Application.Dtos.User;

namespace ProyectoDeAprendizajeP3.Core.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto?> LoginAsync(LoginDto dto);
        Task<UserDto> AddAsync(SaveUserDto dto);
        Task<bool> UpdateAsync(SaveUserDto dto);
        Task<bool> DeleteAsync(int id);
        Task<List<UserDto>> GetAll();
        Task<List<UserDto>> GetAllWithInclude();
        Task<UserDto?> GetById(int id);
       


    }
}
