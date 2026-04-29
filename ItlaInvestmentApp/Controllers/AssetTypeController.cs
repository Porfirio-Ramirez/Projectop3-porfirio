using Microsoft.AspNetCore.Mvc;
using ProyectoDeAprendizajeP3.Core.Application.Dtos.AssetType;
using ProyectoDeAprendizajeP3.Core.Application.Interfaces;
using ProyectoDeAprendizajeP3.Core.Application.Services;
using ProyectoDeAprendizajeP3.Core.Application.ViewModels.AssetType;
using ProyectoDeAprendizajeP3.Infrastruture.Persistence.Contexts;

namespace ItlaInvestmentApp.Controllers
{
    public class AssetTypeController : Controller
    {
        private readonly IAssetTypeService _assetTypeService;
        private readonly IUserSession _usersession;

        public AssetTypeController(IAssetTypeService assetTypeService, IUserSession userSession)
        {
            _assetTypeService = assetTypeService;
            _usersession = userSession;
        }
        public async Task<IActionResult> Index()
        {
            if (!_usersession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (!_usersession.IsAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }
            var dtos = await _assetTypeService.GetAllWithInclude();

            var listEntityVms = dtos.Select(s =>
              new AssetTypeViewModel()
              {
                  Id = s.Id,
                  Name = s.name,
                  Description = s.description,
                  AssetQuantity = s.AssetQuantity
              }).ToList();

            return View(listEntityVms);
        }

        public IActionResult Create()
        {
            if (!_usersession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (!_usersession.IsAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }

            return View("Save", new SaveAssetTypeViewModel() { Name = "" });
        }

        [HttpPost]
        public async Task<IActionResult?> Create(SaveAssetTypeViewModel vm)
        {
            if (!_usersession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (!_usersession.IsAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }
            if (!ModelState.IsValid)
            {
                return View("Save", vm);


            }

            AssetTypeDto dto = new()
            {
                Id = 0,
                name = vm.Name,
                description = vm.Description
            };

            await _assetTypeService.AddAsync(dto);
            return RedirectToRoute(new { Controller = "AssetType", Action = "Index" });

        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!_usersession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (!_usersession.IsAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }

            if (!ModelState.IsValid)
            {
                return RedirectToRoute(new { controller = "AssetType", action = "Index" });
            }

            ViewBag.EditMode = true;
            var dto = await _assetTypeService.GetById(id);
            if (dto == null)
            {
                return RedirectToRoute(new { Controller = "Save", Action = "Index" });
            }

            SaveAssetTypeViewModel vm = new()
            {
                Id = dto.Id,
                Name = dto.name,
                Description = dto.description

            };

            
            return View("Save", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveAssetTypeViewModel vm)
        {
            if (!_usersession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (!_usersession.IsAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }
            if (!ModelState.IsValid)
            {
                ViewBag.EditMode = true;
                return View("Save", vm);
            }

            AssetTypeDto dto = new()
            {
                Id = vm.Id,
                name = vm.Name,
                description = vm.Description
            };

            await _assetTypeService.UpdateAsync(dto);

            return RedirectToRoute(new { Controller = "AssetType", Action = "Index" });
        }

        
        public async Task<IActionResult> Delete(int id)
        {
            if (!_usersession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (!_usersession.IsAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }
            if (!ModelState.IsValid)
            {
                return RedirectToRoute(new { controller = "AssetType", action = "Index" });
            }

            var dto = await _assetTypeService.GetById(id);
            if (dto == null)
            {
                return RedirectToRoute(new { Controller = "AssetType", Action = "Index" });
            }

            DeleteAssetTypeViewModel vm = new()
            {
                Id = dto.Id,
                Name = dto.name
            };

            return View(vm);
            
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteAssetTypeViewModel  vm)
        {
            if (!_usersession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (!_usersession.IsAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }
            if (!ModelState.IsValid)
            {
                ViewBag.EditMode = true;
                return View(vm);
            }


            await _assetTypeService.DeleteAsync(vm.Id);
            return RedirectToRoute(new { Controller = "AssetType", Action = "Index" });
        }
    }
}
