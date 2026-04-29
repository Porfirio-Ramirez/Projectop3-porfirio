using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ProyectoDeAprendizajeP3.Core.Domain.Entities;

namespace ProyectoDeAprendizajeP3.Infrastruture.Persistence.Contexts
{
    public class InvestmentContext : DbContext
    {
        public InvestmentContext(DbContextOptions<InvestmentContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<AssetHistory> assetHistories { get; set; }
        public DbSet<AssetType> assetTypes{ get; set; }
        public DbSet<InvestmentAsset> investmentAssets { get; set; }
        public DbSet<InvestmentPortfolio> investmentPortfolios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }

   
}
