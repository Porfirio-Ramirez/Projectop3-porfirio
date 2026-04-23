using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyectoDeAprendizajeP3.Infrastruture.Persistence.Contexts;

namespace ProyectoDeAprendizajeP3.Infrastruture.Persistence.Repositories
{
    public class AssetRepository
    {
        private readonly InvestmentContext _context;

        public AssetRepository(InvestmentContext context)
        {
            _context = context;
        }

        public async Task<Asset> AddAsync(Asset asset)
        {
            await _context.Set<Asset>().AddAsync(asset);
            await _context.SaveChangesAsync();
            return asset;
        }

        public async Task<Asset?> UpadateAsync(int id, Asset asset)
        {
            var entry = await _context.Set<Asset>().FindAsync(id);

            if(entry != null)
            {
                _context.Entry(entry).CurrentValues.SetValues(asset);
                await _context.SaveChangesAsync();
                return entry;
            }
            return null;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<Asset>().FindAsync(id);

            if (entity != null)
            {
                _context.Set<Asset>().Remove(entity);
                await _context.SaveChangesAsync();

            }
            {
                
            }
        }

        public async Task<List<Asset>> GetAllList()
        {
            return await _context.Set<Asset>().ToListAsync();
        }

        public async Task<Asset?> GetId(int id)
        {
            return await _context.Set<Asset>().FindAsync(id);
        }

        public  IQueryable<Asset> GetAllQuery()
        {
            return _context.Set<Asset>().AsQueryable();
        }
    }
}
