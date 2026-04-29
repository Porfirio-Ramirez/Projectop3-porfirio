using Microsoft.EntityFrameworkCore;
using ProyectoDeAprendizajeP3.Core.Application.Dtos.Asset;
using ProyectoDeAprendizajeP3.Core.Application.Dtos.AssetHistory;
using ProyectoDeAprendizajeP3.Core.Application.Dtos.AssetType;
using ProyectoDeAprendizajeP3.Core.Application.Interfaces;
using ProyectoDeAprendizajeP3.Core.Domain.Common.Enum;
using ProyectoDeAprendizajeP3.Core.Domain.Entities;
using ProyectoDeAprendizajeP3.Core.Domain.Interfaces;


namespace ProyectoDeAprendizajeP3.Core.Application.Services
{
    public class AssetService : IAssetService
    {
        private readonly IAssetRepository _assetRepository;
        private readonly IInvestmentAssetRepository _investmentAssetRepository;

        public AssetService(IAssetRepository assetRepository, IInvestmentAssetRepository investmentAssetRepository)
        {
            _assetRepository = assetRepository;
            _investmentAssetRepository = investmentAssetRepository;
        }

        public async Task<bool> AddAsync(AssetDto dto)
        {
            try
            {
                Asset entity = new()
                {
                    Id = 0,
                    name = dto.name,
                    description = dto.description,
                    AssetTypeId = dto.AssetTypeId,
                    symbol = dto.symbol
                };

                Asset? returnEntity = await _assetRepository.AddAsync(entity);
                if (returnEntity == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<bool> UpdateAsync(AssetDto dto)
        {
            try
            {
                Asset entity = new()
                {
                    Id = dto.Id,
                    name = dto.name,
                    description = dto.description,
                    AssetTypeId = dto.AssetTypeId,
                    symbol = dto.symbol

                };

                Asset? returnEntity = await _assetRepository.UpdateAsync(entity.Id, entity);
                if (returnEntity == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await _assetRepository.DeleteAsync(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<AssetDto?> GetById(int id)
        {
            try
            {
                var listEntityQuery = _assetRepository.GetAllQueryWithInclude(["AssetType"]);

                var entiry = await listEntityQuery.FirstOrDefaultAsync(a => a.Id == id);
                if (entiry == null)
                {
                    return null;
                }

                var dto = new AssetDto
                {
                    Id = entiry.Id,
                    name = entiry.name,
                    description = entiry.description,
                    symbol = entiry.symbol,
                    AssetTypeId = entiry.AssetTypeId,
                    AssetType = entiry.AssetType == null ? null : new AssetTypeDto
                    {
                        Id = entiry.AssetType.Id,
                        name = entiry.AssetType.name,
                        description = entiry.AssetType.description
                    }

                };

                return dto;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<AssetDto>> GetAll()
        {
            try
            {
                var listentity = await _assetRepository.GetAllList();

                var listentitydto = listentity.Select(b => new AssetDto()
                {
                    Id = b.Id,
                    name = b.name,
                    description = b.description,
                    symbol = b.symbol,
                    AssetTypeId = b.AssetTypeId
                }).ToList();

                return listentitydto;

            }
            catch (Exception)
            {
                return [];
            }
        }

        public async Task<List<AssetDto>> GetAllWithInclude()
        {
            try
            {
                var listEntity = _assetRepository.GetAllQueryWithInclude(["AssetType", "AssetHistories"]);

              

                var listEntityDtos = await listEntity.Select(s =>
                new AssetDto()
                {
                    Id = s.Id,
                    name = s.name,
                    description = s.description,
                    symbol =  s.symbol,
                    AssetTypeId = s.AssetTypeId,
                    AssetType = s.AssetType == null ? null : new AssetTypeDto()
                    {
                        Id = s.AssetType.Id,
                        name = s.AssetType.name,
                        description= s.AssetType.name
                    },
                    AssetHistories = s.AssetHistories == null
                    ? new List<AssetHistoryDto>()
                    : s.AssetHistories
                    .OrderByDescending(ah => ah.HistoryValueDate)
                    .Select(s => new AssetHistoryDto()
                    {
                        AssetId = s.AssetId,
                        Id = s.Id,
                        HistoryValueDate = s.HistoryValueDate,
                        Value = s.value
                    }).ToList()

                }).ToListAsync();

                return listEntityDtos;
            }
            catch (Exception)
            {
                return [];
            }

        }

        public async Task<List<AssetForPortfolioDto>> GetAllAssetsByPortfolioId(int portfolioId, string? assetName = null, int? assetTypeId = null, int? assetOrderBy = null)
        {
             try
            {
                var assetIds = await _investmentAssetRepository
                    .GetAllQuery()
                    .Where(ia => ia.InvestmentPortfolioId == portfolioId)
                    .Select(s => s.AssetId).ToListAsync();

                if (assetIds.Count == 0)
                {
                    return [];
                }

                var listEntitiesQuery = _assetRepository
                    .GetAllQueryWithInclude(["AssetType", "AssetHistories"])
                    .Where(w => assetIds.Contains(w.Id));

                var listEntityDtos = listEntitiesQuery.Select(s =>
                new AssetForPortfolioDto()
                {
                    Id = s.Id,
                    name = s.name,
                    description = s.description,
                    Symbol = s.symbol,
                    AssetTypeId = s.AssetTypeId,
                    AssetType = s.AssetType == null ? null : new AssetTypeDto()
                    {
                        Id = s.AssetType.Id,
                        name = s.AssetType.name,
                        description = s.AssetType.description
                    },
                    CurrentValue = s.AssetHistories != null && s.AssetHistories.Any()
                    ? s.AssetHistories
                    .OrderByDescending(ah => ah.HistoryValueDate)
                    .Select(s => new AssetHistoryDto()
                    {
                        AssetId = s.AssetId,
                        Id = s.Id,
                        HistoryValueDate = s.HistoryValueDate,
                        Value = s.value
                    }).First().Value
                    : 0,
                });

                if (!string.IsNullOrWhiteSpace(assetName))
                {
                    listEntityDtos = listEntityDtos.Where(w => w.name.Contains(assetName));
                }

                if (assetTypeId.HasValue)
                {
                    listEntityDtos = listEntityDtos.Where(w => w.AssetTypeId == assetTypeId);
                }

                var listDtos = await listEntityDtos.ToListAsync();

                if (assetOrderBy.HasValue)
                {
                    var listOrderDtos = assetOrderBy switch
                    {
                        (int) AssetOrdered.BY_NAME => listDtos.OrderBy(o => o.name),
                        (int) AssetOrdered.BY_CURRENT_VALUE => listDtos.OrderByDescending(o => o.CurrentValue),
                        _ => listDtos.OrderBy(o => o.name),
                    };

                    listDtos = listOrderDtos.ToList();
                }
                else
                {
                    listDtos = listDtos.OrderBy(o => o.name).ToList();
                }

                return listDtos;
            }
            catch (Exception)
            {
                return [];
            }
        }
        }
    }

