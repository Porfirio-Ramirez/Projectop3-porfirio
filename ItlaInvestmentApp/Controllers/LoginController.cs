using Microsoft.AspNetCore.Mvc;
using ProyectoDeAprendizajeP3.Core.Application.Dtos.User;
using ProyectoDeAprendizajeP3.Core.Application.Interfaces;
using ProyectoDeAprendizajeP3.Core.Application.ViewModels.User;
using ProyectoDeAprendizajeP3.Core.Domain.Common.Enum;
using ProyectoDeAprendizajeP3.Core.Application.Helpers;


namespace ItlaInvestmentApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserSession _userSession;

        public LoginController(IUserService userService, IUserSession userSession)
        {
            _userService = userService;
            _userSession = userSession;
        }

        public ActionResult Index()
        {
            if (_userSession.HasUser())
            {
                UserViewModel? usersession = _userSession.GetUserSession();

                if (usersession != null)
                {
                    return usersession.Role switch
                    {
                        (int)Rol.ADMIN => RedirectToRoute(new { controller = "Home", action = "Index" }),
                        (int)Rol.INVESTOR => RedirectToRoute(new { controller = "InvestorHome", action = "Index" }),
                        _ => RedirectToRoute(new { controller = "Login", action = "Index" }),
                    };
                }
              
            }
            return View(new LoginViewModel() { Password = "", UserName = "" });
        }

        [HttpPost]
        public async Task<ActionResult> Index(LoginViewModel vm)
        {
            if (_userSession.HasUser())
            {
                UserViewModel? usersession = _userSession.GetUserSession();

                if(usersession != null)
                {
                    return usersession.Role switch
                    {
                        (int)Rol.ADMIN => RedirectToRoute(new { controller = "Home", action = "Index" }),
                        (int)Rol.INVESTOR => RedirectToRoute(new { controller = "InvestorHome", action = "Index" }),
                        _ => RedirectToRoute(new { controller = "Login", action = "Index" }),
                    };
                }

                if (!ModelState.IsValid)
                {
                    vm.Password = "";
                    return View(vm);
                }

                UserDto? userdto = await _userService.LoginAsync( new LoginDto()
                {
                    Password = vm.Password,
                    UserName = vm.UserName
                });

                if (userdto != null)
                {
                    UserViewModel uservm = new() 
                    { 
                        Id = userdto.Id,
                        Name = userdto.Name,
                        LastName = userdto.LastName,
                        Email = userdto.Email,
                        UserName = userdto.UserName,
                        Role = userdto.Role, 
                        Phone = userdto.Phone,
                        ProfileImage = userdto.ProfileImage
                    };

                    HttpContext.Session.Set<UserViewModel>("User", uservm);

                    if (uservm.Role == (int)Rol.ADMIN)
                    {
                        return RedirectToRoute(new { controller = "Home", action = "Index" });
                    }

                    return RedirectToRoute(new { controller = "InvestorHome", action = "Index" });
                }
                else
                {
                    ModelState.AddModelError("userValidation", "Data access is incorrect");
                }
            }
            vm.Password = "";
            return View(vm);
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Remove("User");

            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }

        public ActionResult Register()
        {
            return View(new RegisterUserViewModel()
            {
                ConfirmPassword = "",
                Email = "",
                LastName = "",
                Name = "",
                Password = "",
                UserName = "",
            });
        }
        [HttpPost]
        public ActionResult Register(RegisterUserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            SaveUserDto dto = new()
            {
                Id = 0,
                Name = vm.Name,
                LastName = vm.LastName,
                Email = vm.Email,
                UserName = vm.UserName,
                Role = (int)Rol.INVESTOR,
                Password = vm.Password,
                Phone = vm.Phone
            };
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }

        public IActionResult AccessDenied()
        {
            if (_userSession.HasUser())
            {
                return View();
            }

            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }

    }
    }

