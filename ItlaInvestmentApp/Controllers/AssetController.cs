using Microsoft.AspNetCore.Mvc;
using ProyectoDeAprendizajeP3.Core.Application.Dtos.Asset;
using ProyectoDeAprendizajeP3.Core.Application.Interfaces;
using ProyectoDeAprendizajeP3.Core.Application.ViewModels.Asset;
using ProyectoDeAprendizajeP3.Core.Application.ViewModels.AssetHistory;
using ProyectoDeAprendizajeP3.Core.Application.ViewModels.AssetType;


namespace ItlaInvestmentApp.Controllers
{
    public class AssetController : Controller
    {
        private readonly IAssetService _assetservice;
        private readonly IAssetTypeService _assetTypeService;
        private readonly IUserSession _usersession;
        public AssetController(IAssetService assetService, IAssetTypeService assetTypeService, IUserSession userSession)
        {
            _assetservice = assetService;
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
                   },
                   AssetHistories = s.AssetHistories == null
                     ? []
                     : s.AssetHistories
                     .Select(s => new AssetHistoryViewModel()
                     {
                         AssetId = s.AssetId,
                         Id = s.Id,
                         HistoryValueDate = s.HistoryValueDate,
                         Value = s.Value
                     }).ToList()
               }).ToList();

            return View(listEntityVms);
        }

        public async Task<IActionResult> Create()
        {
            if (!_usersession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (!_usersession.IsAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }

            ViewBag.assetTypes = await _assetTypeService.GetAll();
            return View("Save", new SaveAssetViewModel() { Name = "", Symbol = "", AssetTypeId = null });
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveAssetViewModel vm)
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
            if (!_usersession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (!_usersession.IsAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenied" });
            }

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
                return RedirectToRoute(new { controller = "Asset", action = "Index" });
            }
            
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
                
                return View(vm);
            }


            await _assetservice.DeleteAsync(vm.Id);
            return RedirectToRoute(new { Controller = "Asset", Action = "Index" });


        }


    }
}

