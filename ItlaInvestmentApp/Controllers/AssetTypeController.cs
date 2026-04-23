using Microsoft.AspNetCore.Mvc;
using ProyectoDeAprendizajeP3.Core.Application.Dtos.AssetType;
using ProyectoDeAprendizajeP3.Core.Application.Services;
using ProyectoDeAprendizajeP3.Core.Application.ViewModels.AssetType;
using ProyectoDeAprendizajeP3.Infrastruture.Persistence.Contexts;

namespace ItlaInvestmentApp.Controllers
{
    public class AssetTypeController : Controller
    {
        private readonly AssetTypeService _assetTypeService;

        public AssetTypeController(InvestmentContext investmentContext)
        {
            _assetTypeService = new AssetTypeService(investmentContext);
        }
        public async Task<IActionResult> Index()
        {
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

            return View("Save", new SaveAssetTypeViewModel() { Name = "" });
        }

        [HttpPost]
        public async Task<IActionResult?> Create(SaveAssetTypeViewModel vm)
        {
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
