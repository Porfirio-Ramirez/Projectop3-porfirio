using Microsoft.AspNetCore.Mvc;
using ProyectoDeAprendizajeP3.Core.Application.Dtos.User;
using ProyectoDeAprendizajeP3.Core.Application.Interfaces;
using ProyectoDeAprendizajeP3.Core.Application.ViewModels.User;
using ItlaInvestmentApp.Helpers;


namespace ItlaInvestmentApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserSession _userSession;

        public UserController(IUserService userService, IUserSession userSession)
        {
            _userService = userService;
            _userSession = userSession;
        }
        public async Task<IActionResult> Index()
        {
            if (!_userSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (!_userSession.IsAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }
            var dtos = await _userService.GetAllWithInclude();

            var listEntityVms = dtos.Select(s =>
              new UserViewModel()
              {
                  Id = s.Id,
                  Name = s.Name,
                  Email = s.Email,
                  LastName = s.LastName,
                  Role = s.Role,
                  Phone = s.Phone,
                  ProfileImage = s.ProfileImage,
                  UserName = s.UserName
              }).ToList();

            return View(listEntityVms);
        }

        public IActionResult Create()
        {
            if (!_userSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (!_userSession.IsAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }
            return View(new CreateUserViewModel() { Id = 0, Name = "", Email = "", LastName = "", Password = "", Role = 0, UserName = "", ConfirmPassword = ""});
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel vm)
        {
            if (!_userSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (!_userSession.IsAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            SaveUserDto dto = new()
            {
                Id = 0,
                Name = vm.Name,
                Email = vm.Email,
                LastName = vm.LastName,
                Password = vm.Password,
                Role = vm.Role,
                Phone = vm.Phone,
                ProfileImage = "vm.ProfileImageFile",
                UserName = vm.UserName
                
            };
           UserDto? returnUser = await _userService.AddAsync(dto);

            if (returnUser != null && returnUser.Id != 0)
            {
                dto.Id = returnUser.Id;
                dto.ProfileImage = FileManager.Upload(vm.ProfileImageFile, dto.Id, "Users");
                await _userService.UpdateAsync(dto);
            }

            
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!_userSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (!_userSession.IsAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }

            if (!ModelState.IsValid)
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            ViewBag.EditMode = true;
            var dto = await _userService.GetById(id);

            if (dto == null)
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            UpdateUserViewModel vm = new()
            {
                Id = dto.Id,
                Name = dto.Name,
                Email = dto.Email,
                LastName = dto.LastName,
                Password = "",
                Role = dto.Role,
                Phone = dto.Phone,
                UserName = dto.UserName
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateUserViewModel vm)
        {
            if (!_userSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (!_userSession.IsAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }

            if (!ModelState.IsValid)
            {
                ViewBag.EditMode = true;
                return View(vm);
            }

            SaveUserDto dto = new()
            {
                Id = vm.Id,
                Name = vm.Name,
                Email = vm.Email,
                LastName = vm.LastName,
                Password = vm.Password ?? "",
                Role = vm.Role,
                Phone = vm.Phone,
                UserName = vm.UserName
            };
           var currenDto = await _userService.GetById(vm.Id);
            string? currentImagepath = "";

            if (currenDto != null)
            {
                currentImagepath = currenDto.ProfileImage;
            }

            dto.ProfileImage = FileManager.Upload(vm.ProfileImageFile, dto.Id, "Users", true, currentImagepath);
            await _userService.UpdateAsync(dto);
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_userSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (!_userSession.IsAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }

            if (!ModelState.IsValid)
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            var dto = await _userService.GetById(id);
            if (dto == null)
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            DeleteUserViewModel vm = new() { Id = dto.Id, Name = dto.Name, LastName = dto.LastName };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteUserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await _userService.DeleteAsync(vm.Id);
            FileManager.Delete(vm.Id, "Users");
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }
    }
}

