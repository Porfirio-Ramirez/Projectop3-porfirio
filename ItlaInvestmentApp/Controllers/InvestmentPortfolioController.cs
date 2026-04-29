using Microsoft.AspNetCore.Mvc;
using ProyectoDeAprendizajeP3.Core.Application.Dtos.InvestmentPortfolio;
using ProyectoDeAprendizajeP3.Core.Application.Interfaces;
using ProyectoDeAprendizajeP3.Core.Application.ViewModels.Asset;
using ProyectoDeAprendizajeP3.Core.Application.ViewModels.AssetType;
using ProyectoDeAprendizajeP3.Core.Application.ViewModels.InvestmentPortfolio;
using ProyectoDeAprendizajeP3.Core.Application.ViewModels.User;

namespace ItlaInvestmentApp.Controllers
{
    public class InvestmentPortfolioController : Controller
    {
        private readonly IInvestmentPortfolioService _investmentPortfolioService;
        private readonly IUserService _userService;
        private readonly IAssetService _assetService;
        private readonly IAssetTypeService _assetTypeService;

        public InvestmentPortfolioController(IInvestmentPortfolioService investmentPortfolioService, IUserService userService, IAssetTypeService assetTypeService, IAssetService assetService)
        {
            _investmentPortfolioService = investmentPortfolioService;
            _userService = userService;
            _assetTypeService = assetTypeService;
            _assetService = assetService;
        }
        public async Task<IActionResult> Index()
        {
            var dtos = await _investmentPortfolioService.GetAllWithInclude();

            var listEntityVms = dtos.Select(s =>
              new InvestmentPortfolioViewModel()
              {
                  Id = s.Id,
                  Name = s.name,
                  Description = s.description,
                  UserId = s.UserId,
                  User = s.user == null ? null : new UserViewModel()
                  {
                      Id = s.user.Id,
                      Name = s.user.Name,
                      Email = s.user.Email,
                      LastName = s.user.LastName,
                      Role = s.user.Role,
                      Phone = s.user.Phone,
                      ProfileImage = s.user.ProfileImage
                  }
              }).ToList();

            return View(listEntityVms);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Users = await _userService.GetAll();
            return View("Save", new SaveInvestmentPortfolioViewModel() { Name = "" });
        }

        public async Task<IActionResult> AssetsDetails(int portfolioId, string? assetName = null, int? assetTypeId = null, int? assetOrderBy = null)
        {
            var portfolioDto = await _investmentPortfolioService.GetById(portfolioId);

            if (portfolioDto == null)
            {
                return RedirectToRoute(new { controller = "InvestmentPortfolio", action = "Index" });
            }

            InvestmentPortfolioViewModel portfolioVm = new()
            {
                Id = portfolioDto.Id,
                Name = portfolioDto.name,
                Description = portfolioDto.description,
                UserId = portfolioDto.UserId
            };

            var dtos = await _assetService.GetAllAssetsByPortfolioId(portfolioId, assetName, assetTypeId, assetOrderBy);

            var listEntityVms = dtos.Select(s =>
         new AssetForPortfolioViewModel()
         {
             Id = s.Id,
             Name = s.name,
             Description = s.description,
             Symbol = s.Symbol,
             AssetTypeId = s.AssetTypeId,
             AssetType = s.AssetType == null ? null : new AssetTypeViewModel()
             {
                 Id = s.AssetType.Id,
                 Name = s.AssetType.name,
                 Description = s.AssetType.description
             },
             CurrentValue = s.CurrentValue,
         }).ToList();

            ViewBag.Portfolio = portfolioVm;
            ViewBag.AssetTypes = await _assetTypeService.GetAll();

            return View("Details", listEntityVms);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveInvestmentPortfolioViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Users = await _userService.GetAll();
                return View("Save", vm);
            }

            InvestmentPortfolioDto dto = new()
            {
                Id = 0,
                name = vm.Name,
                description = vm.Description,
                UserId = vm.UserId,
            };

            await _investmentPortfolioService.AddAsync(dto);
            return RedirectToRoute(new { controller = "InvestmentPortfolio", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToRoute(new { controller = "InvestmentPortfolio", action = "Index" });
            }

            ViewBag.EditMode = true;
            ViewBag.Users = await _userService.GetAll();
            var dto = await _investmentPortfolioService.GetById(id);

            if (dto == null)
            {
                return RedirectToRoute(new { controller = "InvestmentPortfolio", action = "Index" });
            }

            SaveInvestmentPortfolioViewModel vm = new()
            {
                Id = dto.Id,
                Name = dto.name,
                Description = dto.description,
                UserId = dto.UserId
            };
            return View("Save", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveInvestmentPortfolioViewModel vm)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.EditMode = true;
                ViewBag.Users = await _userService.GetAll();
                return View("Save", vm);
            }

            InvestmentPortfolioDto dto = new()
            {
                Id = vm.Id,
                name = vm.Name,
                description = vm.Description,
                UserId = vm.UserId
            };
            await _investmentPortfolioService.UpdateAsync(dto);
            return RedirectToRoute(new { controller = "InvestmentPortfolio", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToRoute(new { controller = "InvestmentPortfolio", action = "Index" });
            }
            var dto = await _investmentPortfolioService.GetById(id);
            if (dto == null)
            {
                return RedirectToRoute(new { controller = "InvestmentPortfolio", action = "Index" });
            }
            DeleteInvestmentPortfolioViewModel vm = new() { Id = dto.Id, Name = dto.name };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteInvestmentPortfolioViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await _investmentPortfolioService.DeleteAsync(vm.Id);
            return RedirectToRoute(new { controller = "InvestmentPortfolio", action = "Index" });
        }

    }
}

