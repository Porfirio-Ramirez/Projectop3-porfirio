using Microsoft.AspNetCore.Mvc;
using ProyectoDeAprendizajeP3.Core.Application.Dtos.User;
using ProyectoDeAprendizajeP3.Core.Application.Interfaces;
using ProyectoDeAprendizajeP3.Core.Application.ViewModels.User;

namespace ItlaInvestmentApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> Index()
        {
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
                  ProfileImage = s.ProfileImage
              }).ToList();

            return View(listEntityVms);
        }

        public IActionResult Create()
        {
            return View(new CreateUserViewModel() { Id = 0, Name = "", Email = "", LastName = "", Password = "", Role = 0, UserName = "" });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel vm)
        {
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
                ProfileImage = vm.ProfileImage,
                UserName = vm.UserName
                
            };
            await _userService.AddAsync(dto);
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
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
                ProfileImage = dto.ProfileImage,
                UserName = dto.UserName
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateUserViewModel vm)
        {
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
                ProfileImage = vm.ProfileImage,
                UserName = vm.UserName
            };
            await _userService.UpdateAsync(dto);
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
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
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }
    }
}

