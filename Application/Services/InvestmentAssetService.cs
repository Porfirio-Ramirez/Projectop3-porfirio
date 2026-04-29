

using Microsoft.EntityFrameworkCore;
using ProyectoDeAprendizajeP3.Core.Application.Dtos.Asset;
using ProyectoDeAprendizajeP3.Core.Application.Dtos.InvestmentAsset;
using ProyectoDeAprendizajeP3.Core.Application.Dtos.InvestmentPortfolio;
using ProyectoDeAprendizajeP3.Core.Application.Interfaces;
using ProyectoDeAprendizajeP3.Core.Domain.Entities;
using ProyectoDeAprendizajeP3.Core.Domain.Interfaces;

namespace ProyectoDeAprendizajeP3.Core.Application.Services
{
    public class InvestmentAssetService : IInvestmentAssetService
    {
        private readonly IInvestmentAssetRepository _investmentAssetRepository;

        public InvestmentAssetService(IInvestmentAssetRepository investmentAssetRepository)
        {
            _investmentAssetRepository = investmentAssetRepository;
        }
        public async Task<bool> AddAsync(InvestmentAssetDto dto)
        {
            try
            {
                InvestmentAsset entity = new()
                {
                    Id = 0,
                    AssetId = dto.AssetId,
                    InvestmentPortfolioId = dto.InvestmentPortfolioId,
                    associationdate = dto.AssociationDate
                };

                InvestmentAsset? returnEntity = await _investmentAssetRepository.AddAsync(entity);
                if (returnEntity == null)
                {
                    return false;
                }
                return true;

            }catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await _investmentAssetRepository.DeleteAsync(id);
                return true;
            }catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<InvestmentAssetDto>> GetAll()
        {
            try
            {
                var entity = await _investmentAssetRepository.GetAllList();

                var entities = entity.Select(n =>
                new InvestmentAssetDto()
                {
                    Id = n.Id,
                    AssetId = n.AssetId,
                    InvestmentPortfolioId = n.InvestmentPortfolioId,
                    AssociationDate = n.associationdate
                }).ToList();
                return entities;
            }catch (Exception)
            {
                return [];
            }
        }

        public async Task<List<InvestmentAssetDto>> GetAllWithInclude()
        {
            try
            {
                var entity = _investmentAssetRepository.GetAllQueryWithInclude(["Asset", "investmentAssets"]);

                var entitiesdto = await entity.Select(a =>
                new InvestmentAssetDto()
                {
                    Id = a.Id,
                    AssetId = a.AssetId,
                    AssociationDate = a.associationdate,
                    InvestmentPortfolioId = a.InvestmentPortfolioId,
                    Asset = a.asset == null ? null : new AssetDto()
                    {
                        Id = a.asset.Id,
                        name = a.asset.name,
                        description = a.asset.description,
                        AssetTypeId = a.asset.AssetTypeId,
                        symbol = a.asset.symbol
                    },
                    InvestmentPortfolio = a.investmentPortfolio == null ? null : new InvestmentPortfolioDto()
                    {
                        Id = a.investmentPortfolio.Id,
                        name = a.investmentPortfolio.name,
                        description = a.investmentPortfolio.description,
                        UserId = a.investmentPortfolio.UserId
                    }

                }).ToListAsync();

                return entitiesdto;

            }catch (Exception)
            {
                return [];
            }
        }

        public async Task<InvestmentAssetDto?> GetByAssetAndPortfolioAsync(int assetId, int portfolioId)
        {
            try
            {
                var investmentAsset = await _investmentAssetRepository
                    .GetAllQueryWithInclude(["Asset"])
                    .FirstOrDefaultAsync(ia => ia.AssetId == assetId
                    && ia.InvestmentPortfolioId == portfolioId);

                if (investmentAsset == null)
                {
                    return null;
                }

                InvestmentAssetDto dto = new()
                {
                    AssetId = investmentAsset.AssetId,
                    Id = investmentAsset.Id,
                    InvestmentPortfolioId = investmentAsset.InvestmentPortfolioId,
                    Asset = investmentAsset.asset == null ? null : new()
                    {
                        Id = investmentAsset.asset.Id,
                        AssetTypeId = investmentAsset.asset.AssetTypeId,
                        name = investmentAsset.asset.name,
                        symbol = investmentAsset.asset.symbol
                    }
                };

                return dto;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<InvestmentAssetDto?> GetById(int id)
        {

            try
            {
                var entity = _investmentAssetRepository.GetAllQueryWithInclude(["Asset", "investmentAssets"]);

                var entities = await entity.FirstOrDefaultAsync(a => a.Id == id);

                if (entities == null)
                {
                    return null;
                }

                var dto = new InvestmentAssetDto()
                {
                    Id = entities.Id,
                    AssetId = entities.AssetId,
                    InvestmentPortfolioId = entities.InvestmentPortfolioId,
                    AssociationDate = entities.associationdate,
                    Asset = entities.asset == null ? null : new AssetDto()
                    {
                        Id = entities.asset.Id,
                        name = entities.asset.name,
                        description = entities.asset.description,
                        AssetTypeId = entities.asset.AssetTypeId,
                        symbol = entities.asset.symbol
                    },
                    InvestmentPortfolio = entities.investmentPortfolio == null ? null : new InvestmentPortfolioDto()
                    {
                        Id = entities.investmentPortfolio.Id,
                        name = entities.investmentPortfolio.name,
                        description = entities.investmentPortfolio.description,
                        UserId = entities.investmentPortfolio.UserId,
                    },
                };
                return dto;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdateAsync(InvestmentAssetDto dto)
        {
            try
            {
                InvestmentAsset entity = new()
                {
                    Id = dto.Id,
                    AssetId = dto.AssetId,
                    InvestmentPortfolioId = dto.InvestmentPortfolioId,
                    associationdate = dto.AssociationDate
                };

                InvestmentAsset? returnEntity = await _investmentAssetRepository.UpdateAsync(entity.Id, entity);

                if (returnEntity == null)
                {
                    return false;
                }
                return true;

            }catch (Exception)
            {
                return false;
            }
        }
    }
}
