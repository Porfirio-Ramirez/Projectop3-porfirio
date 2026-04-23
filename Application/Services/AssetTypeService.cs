using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos.AssetType;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Entities;
using Persistence.Repositories;

namespace Application.Services
{
    public class AssetTypeService 
    {
        private readonly AssetTypeRepository _assetTypeRepository;

        public AssetTypeService(InvestmentContext investmentContext)
        {
            _assetTypeRepository = new AssetTypeRepository(investmentContext);
        }

        public async Task<bool> AddAsync(AssetTypeDto assetTypeDto)
        {
            try
            {
                AssetType entity = new()
                {
                    Id = assetTypeDto.Id,
                    name = assetTypeDto.name,
                    description = assetTypeDto.description,

                };
                var entities = await _assetTypeRepository.AddAsync(entity);
                if (entities == null)
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

        public async Task<bool> UpdateAsync(AssetTypeDto assetTypeDto)
        {
            try
            {
                AssetType entity = new()
                {
                    Id = assetTypeDto.Id,
                    name = assetTypeDto.name,
                    description = assetTypeDto.description
                };

                AssetType? entities = await _assetTypeRepository.UpdateAsync(entity.Id, entity);
                if(entities == null)
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
                await _assetTypeRepository.DeleteAsync(id);
                return true;
            }catch (Exception)
            {
                return false;
            }
        }

        public async Task<AssetTypeDto?> GetById(int id)
        {
            try
            {
                var entity = await _assetTypeRepository.GetId(id);
                if (entity == null)
                {
                    return null;
                }
                AssetTypeDto dto = new()
                {
                    Id = entity.Id,
                    name = entity.name,
                    description = entity.description
                };
                return dto;
            }catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<AssetTypeDto>> GetAll()
        {
            try
            {
                var entities = await _assetTypeRepository.GetAllList();
                var entitydto = entities.Select(a =>
                 new AssetTypeDto()
                 {
                     Id = a.Id,
                     name = a.name,
                     description = a.description
                 }).ToList();

                return entitydto;
            }
            catch (Exception)
            {
                return [];
            }
        }

        public async Task<List<AssetTypeDto>> GetAllWithInclude()
        {
            try
            {
                var entity =  _assetTypeRepository.GetAllQuery();

                var entitiesdto = await entity.Include(e => e.Assets).ToListAsync();

                var entities = entitiesdto.Select(e =>
                new AssetTypeDto()
                {
                    Id = e.Id,
                    name = e.name,
                    description = e.description,
                    AssetQuantity = e.Assets != null ? e.Assets.Count : 0
                }).ToList();
                return entities;
            }catch (Exception)
            {
                return [];
            }
        }
    }
}
