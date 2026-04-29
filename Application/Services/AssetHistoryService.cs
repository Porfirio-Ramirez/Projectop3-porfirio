using Microsoft.EntityFrameworkCore;
using ProyectoDeAprendizajeP3.Core.Application.Dtos.Asset;
using ProyectoDeAprendizajeP3.Core.Application.Dtos.AssetHistory;
using ProyectoDeAprendizajeP3.Core.Application.Interfaces;
using ProyectoDeAprendizajeP3.Core.Domain.Entities;
using ProyectoDeAprendizajeP3.Core.Domain.Interfaces;

namespace ProyectoDeAprendizajeP3.Core.Application.Services
{
    public class AssetHistoryService : IAssetHistoryService
    {
        private readonly IAssetHistoryRepository _assetHistoryRepository;

        public AssetHistoryService( IAssetHistoryRepository assetHistoryRepository)
        {
            _assetHistoryRepository = assetHistoryRepository;
        }
        public async Task<bool> AddAsync(AssetHistoryDto dto)
        {
            try
            {
                AssetHistory entity = new()
                {
                    Id = 0,
                    AssetId = dto.AssetId,
                    HistoryValueDate = dto.HistoryValueDate,
                    value = dto.Value
                };
                AssetHistory? returnentity = await _assetHistoryRepository.AddAsync(entity);
                if (returnentity == null)
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
                await _assetHistoryRepository.DeleteAsync(id);
                return true;

            }catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<AssetHistoryDto>> GetAll()
        {
            try
            {
                var entities = await _assetHistoryRepository.GetAllList();

                var entitiesdto = entities.Select(s =>
                new AssetHistoryDto()
                {
                    Id = s.Id,
                    AssetId = s.AssetId,
                    HistoryValueDate = s.HistoryValueDate,
                    Value = s.value
                }).ToList();
                return entitiesdto;

            }catch (Exception)
            {
                return [];
            }
        }

        public async Task<List<AssetHistoryDto>> GetAllWithInclude()
        {
            try
            {
                var entities = _assetHistoryRepository.GetAllQueryWithInclude(["Asset"]);

                var entitiesdto = await entities.Select(a =>
                new AssetHistoryDto()
                {
                    Id = a.Id,
                    AssetId = a.AssetId,
                    HistoryValueDate = a.HistoryValueDate,
                    Value = a.value,
                    Asset = a.Assets == null ? null : new AssetDto
                    {
                        Id = a.Assets.Id,
                        name = a.Assets.name,
                        description = a.Assets.description,
                        AssetTypeId = a.Assets.AssetTypeId,
                        symbol = a.Assets.symbol
                    }
                }).ToListAsync();
                
                return entitiesdto;

            }
            catch (Exception)
            {
                return [];
            }
        }

        public async Task<AssetHistoryDto?> GetById(int id)
        {
            try
            {
                var entity = _assetHistoryRepository.GetAllQueryWithInclude(["Asset"]);

                var entities = await entity.FirstOrDefaultAsync(a => a.Id == id);

                if (entities == null)
                {
                    return null;
                }
                var dto = new AssetHistoryDto()
                {
                    Id = entities.Id,
                    AssetId = entities.AssetId,
                    HistoryValueDate = entities.HistoryValueDate,
                    Value = entities.value,
                    Asset = entities.Assets == null ? null : new AssetDto()
                    {
                        Id = entities.Assets.Id,
                        name = entities.Assets.name,
                        description = entities.Assets.description,
                        symbol = entities.Assets.symbol,
                        AssetTypeId = entities.Assets.AssetTypeId
                    }
                };
                return dto;

            }catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdateAsync(AssetHistoryDto assetHistory)
        {
            try
            {
                var entidb = await _assetHistoryRepository.GetById(assetHistory.Id);

                if (entidb == null)
                {
                    return false;
                }

                AssetHistory entity = new()
                {
                    Id = assetHistory.Id,
                    HistoryValueDate = entidb.HistoryValueDate,
                    AssetId = entidb.AssetId,
                    value = assetHistory.Value
                };

                AssetHistory? returnentity = await _assetHistoryRepository.UpdateAsync(entity.Id, entity);
                if (returnentity == null)
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
