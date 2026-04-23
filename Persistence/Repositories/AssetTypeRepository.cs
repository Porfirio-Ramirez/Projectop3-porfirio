using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Entities;

namespace Persistence.Repositories
{
    public class AssetTypeRepository
    {
        private readonly InvestmentContext _context;

        public AssetTypeRepository(InvestmentContext context)
        {
            _context = context;
        }

        public async Task<AssetType> AddAsync(AssetType assetType)
        {
            await _context.Set<AssetType>().AddAsync(assetType);
            await _context.SaveChangesAsync();
            return assetType;
        }

        public async Task<AssetType?> UpdateAsync(int id, AssetType assetType)
        {
            var entry = await _context.Set<AssetType>().FindAsync(id);

            if(entry != null)
            {
                _context.Entry(entry).CurrentValues.SetValues(assetType);
                await _context.SaveChangesAsync();
                return entry;
            }

            return null;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<AssetType>().FindAsync(id);

            if(entity != null)
            {
                _context.Set<AssetType>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<AssetType>> GetAllList()
        {
          return  await _context.Set<AssetType>().ToListAsync();
        }

        public async Task<AssetType?> GetId(int id)
        {
            return await _context.Set<AssetType>().FindAsync(id);
        }

        public  IQueryable<AssetType> GetAllQuery()
        {
           return  _context.Set<AssetType>().AsQueryable();
        }
    }
}
