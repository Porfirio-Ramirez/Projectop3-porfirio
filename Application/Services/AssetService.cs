using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyectoDeAprendizajeP3.Core.Application.Dtos.Asset;
using ProyectoDeAprendizajeP3.Infrastruture.Persistence.Contexts;
using ProyectoDeAprendizajeP3.Infrastruture.Persistence.Repositories;

namespace ProyectoDeAprendizajeP3.Core.Application.Services
{
    public class AssetService
    {
        private readonly AssetRepository _assetRepository;

        public AssetService(InvestmentContext context)
        {
            _assetRepository = new AssetRepository(context); 
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
                if(returnEntity == null)
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

                Asset? returnEntity = await _assetRepository.UpadateAsync(entity.Id, entity);
                if(returnEntity == null)
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
            }catch (Exception)
            {
                return false;
            }
        }

        public async Task<AssetDto?> GetById(int id)
        {
            try
            {
                var listEntityQuery =  _assetRepository.GetAllQuery();

                var entiry = await listEntityQuery.Include(at => at.AssetType)
                                                  .FirstOrDefaultAsync(a => a.Id == id);
                if(entiry == null)
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
                    AssetType = entiry.AssetType == null ? null : new AssetTypeDto{
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

            }catch (Exception)
            {
                return [];
            }
        }

        public async Task<List<AssetDto>> GetAllWithInclude()
        {
            try
            {
                var listEntity = _assetRepository.GetAllQuery();

                var entities = await listEntity.Include(a => a.AssetType).ToListAsync();

                var entityDto = entities.Select(p => new AssetDto()
                {
                    Id = p.Id,
                    name = p.name,
                    description = p.description,
                    symbol = p.symbol,
                    AssetTypeId = p.AssetTypeId,
                    AssetType = p.AssetType == null ? null : new AssetTypeDto()
                    {
                        Id = p.AssetType.Id,
                        name = p.AssetType.name,
                        description = p.AssetType.description
                    }
                }).ToList();
                return entityDto;
            }
            catch (Exception)
            {
                return [];
            }
           
        }
    }
}
