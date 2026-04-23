using Application.Dtos.Asset;
using Application.Dtos.AssetType;
using Application.Services;
using Application.ViewModels.Asset;
using Application.ViewModels.AssetType;
using Microsoft.AspNetCore.Mvc;
using Persistence.Contexts;

namespace ItlaInvestmentApp.Controllers
{
    public class AssetController : Controller
    {
        private readonly AssetService _assetservice;
        private readonly AssetTypeService _assetTypeService;

        public AssetController(InvestmentContext investmentContext)
        {
            _assetservice = new AssetService(investmentContext);
            _assetTypeService = new AssetTypeService(investmentContext);
        }
        public async Task<IActionResult> Index()
        {
            var dtos = await _assetservice.GetAllWithInclude();

            var listEntityVms = dtos.Select(s =>
              new AssetViewModel()
              {
                  Id = s.Id,
                  Name = s.name,
                  Description = s.description,
                  Symbol = s.symbol,
                  AssetTypeId = s.AssetTypeId,
                  AssetType = s.AssetType == null ? null : new AssetTypeViewModel()
                  {
                      Id = s.AssetType.Id,
                      Name = s.AssetType.name,
                      Description = s.AssetType.description
                  }
              }).ToList();

            return View(listEntityVms);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.assetTypes = await _assetTypeService.GetAll();
            return View("Save", new SaveAssetViewModel() { Name = "", Symbol = "", AssetTypeId = null });
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveAssetViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.assetTypes = await _assetTypeService.GetAll();
                return View("Save", vm);
            }

            AssetDto dto = new()
            {
                Id = 0,
                name = vm.Name,
                description = vm.Description,
                AssetTypeId = vm.AssetTypeId ?? 0,
                symbol = vm.Symbol
            };

            await _assetservice.AddAsync(dto);
            return RedirectToRoute(new { Controller = "Asset", Action = "Index" });
        }

       
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.EditMode = true;
            ViewBag.AssetTypes = await _assetTypeService.GetAll();
            var dto = await _assetservice.GetById(id);

            if (dto == null)
            {
                return RedirectToRoute(new { Controller = "Asset", Action = "Index" });
            }

             SaveAssetViewModel vm = new()
            {
                Id = dto.Id,
                Name = dto.name,
                Description = dto.description,
                AssetTypeId = dto.AssetTypeId,
                Symbol = dto.symbol
            };

            return View("Save", vm);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveAssetViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.EditMode = true;
                ViewBag.AssetTypes = await _assetTypeService.GetAll();
                return View("Save", vm);
            }

            AssetDto dto = new()
            {
                Id = vm.Id,
                name = vm.Name,
                description = vm.Description,
                symbol = vm.Symbol,
                AssetTypeId = vm.AssetTypeId ?? 0
            };

            await _assetservice.UpdateAsync(dto);
            return RedirectToRoute(new { Controller = "Asset", Action = "Index" });


        }

       
        public async Task<IActionResult> Delete(int id)
        {
           var dto = await _assetservice.GetById(id);
            if (dto == null)
            {
                return RedirectToRoute(new { Controller = "Asset", Action = "Index" });
            }

            DeleteAssetViewModel vm = new() { Id = dto.Id, Name = dto.name };
            return View(vm);
           


        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteAssetViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                
                return View(vm);
            }


            await _assetservice.DeleteAsync(vm.Id);
            return RedirectToRoute(new { Controller = "Asset", Action = "Index" });


        }


    }
}

