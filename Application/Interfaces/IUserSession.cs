
using ProyectoDeAprendizajeP3.Core.Application.ViewModels.User;

namespace ProyectoDeAprendizajeP3.Core.Application.Interfaces
{
    public interface IUserSession
    {
        UserViewModel? GetUserSession();
        bool HasUser();
        bool IsAdmin();
    }
}
