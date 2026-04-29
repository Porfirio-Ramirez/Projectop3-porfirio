using ProyectoDeAprendizajeP3.Core.Application.Interfaces;
using ProyectoDeAprendizajeP3.Core.Application.ViewModels.User;
using ProyectoDeAprendizajeP3.Core.Application.Helpers;
using ProyectoDeAprendizajeP3.Core.Domain.Common.Enum;

namespace ItlaInvestmentApp.Middlewares
{
    public class UserSersion : IUserSession
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public UserSersion(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public UserViewModel? GetUserSession()
        {

            UserViewModel? userViewModel = _contextAccessor.HttpContext?.Session
                .Get<UserViewModel>("User");
            if (userViewModel == null)
            {
                return null;
            }

            return userViewModel;
        }
        

        public bool HasUser()
        {
            UserViewModel? userViewModel =  _contextAccessor.HttpContext?.Session 
                .Get<UserViewModel>("User");
           if (userViewModel == null)
            {
                return false;
            }

            return true;
        }

        public bool IsAdmin()
        {
            UserViewModel? userViewModel = _contextAccessor.HttpContext?.Session
               .Get<UserViewModel>("User");
            if (userViewModel == null)
            {
                return false;
            }

            return userViewModel.Role == (int)Rol.ADMIN;
        }
    }
}
