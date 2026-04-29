using Microsoft.AspNetCore.Mvc;
using ProyectoDeAprendizajeP3.Core.Application.Dtos.InvestmentAsset;
using ProyectoDeAprendizajeP3.Core.Application.Interfaces;
using ProyectoDeAprendizajeP3.Core.Application.Services;
using ProyectoDeAprendizajeP3.Core.Application.ViewModels.InvestmentAssets;

namespace ItlaInvestmentApp.Controllers
{
    public class InvestmentAssetController : Controller
    {
        private readonly IInvestmentAssetService _investmentAssetService;
        private readonly IAssetService _assetService;
        private readonly IUserSession _usersession;

        public InvestmentAssetController(IInvestmentAssetService investmentAssetService, IAssetService assetService, IUserSession userSession)
        {
            _investmentAssetService = investmentAssetService;
            _assetService = assetService;
            _usersession = userSession;
        }
        public async Task<IActionResult> Create(int portfolioId)
        {
            if (!_usersession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

          

            if (!ModelState.IsValid)
            {
                return RedirectToRoute(new { controller = "InvestmentPortfolio", action = "AssetsDetails", portfolioId });
            }

            ViewBag.Assets = await _assetService.GetAll();
            return View(new SaveInvestmentAssetViewModel() { AssetId = 0, Id = 0, InvestmentPortfolioId = portfolioId });
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveInvestmentAssetViewModel vm)
        {
            if (!_usersession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

           

            if (!ModelState.IsValid)
            {
                ViewBag.Assets = await _assetService.GetAll();
                return View(vm);
            }

            InvestmentAssetDto dto = new()
            {
                Id = 0,
                AssetId = vm.AssetId,
                InvestmentPortfolioId = vm.InvestmentPortfolioId
            };

            await _investmentAssetService.AddAsync(dto);
            return RedirectToRoute(new { controller = "InvestmentPortfolio", action = "AssetsDetails", portfolioId = vm.InvestmentPortfolioId });
        }

        public async Task<IActionResult> Delete(int assetId, int portfolioId)
        {
            if (!_usersession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                return RedirectToRoute(new { controller = "InvestmentPortfolio", action = "AssetsDetails", portfolioId });
            }

            var dto = await _investmentAssetService.GetByAssetAndPortfolioAsync(assetId, portfolioId);
            if (dto == null)
            {
                return RedirectToRoute(new { controller = "InvestmentPortfolio", action = "AssetsDetails", portfolioId });
            }

            DeleteInvestmentAssetViewModel vm = new()
            {
                Id = dto.Id,
                AssetName = dto.Asset?.name,
                PortfolioId = dto.InvestmentPortfolioId,
                AssetId = dto.AssetId
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteInvestmentAssetViewModel vm)
        {
            if (!_usersession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await _investmentAssetService.DeleteAsync(vm.Id);
            return RedirectToRoute(new { controller = "InvestmentPortfolio", action = "AssetsDetails", portfolioId = vm.PortfolioId });
        }
    }
}

