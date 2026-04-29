using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProyectoDeAprendizajeP3.Core.Domain.Entities;


namespace ProyectoDeAprendizajeP3.Infrastruture.Persistence.EntityConfigurations
{
    public class InvestmentAssetEntityConfiguration : IEntityTypeConfiguration<InvestmentAsset>
    {
        public void Configure(EntityTypeBuilder<InvestmentAsset> builder)
        {
            #region Basic Configuration
            builder.HasKey(x => new { x.InvestmentPortfolioId, x.AssetId });
            builder.ToTable("InvestmentAssets");
            #endregion

            #region Relationship
            builder.HasOne<Asset>(a => a.asset)
                   .WithMany(i => i.investmentAssets)
                   .HasForeignKey(i => i.AssetId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<InvestmentPortfolio>(p => p.investmentPortfolio)
                   .WithMany(i => i.investmentAssets)
                   .HasForeignKey(i => i.InvestmentPortfolioId)
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
