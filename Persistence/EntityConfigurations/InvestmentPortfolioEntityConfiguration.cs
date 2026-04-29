using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProyectoDeAprendizajeP3.Core.Domain.Entities;


namespace ProyectoDeAprendizajeP3.Infrastruture.Persistence.EntityConfigurations
{
    public class InvestmentPortfolioEntityConfiguration : IEntityTypeConfiguration<InvestmentPortfolio>
    {
        public void Configure(EntityTypeBuilder<InvestmentPortfolio> builder)
        {
            #region Basic Configuration
            builder.HasKey(x => x.Id);
            builder.ToTable("InvestmentPortfolios");
            #endregion

            #region Property Configuration
            builder.Property(p => p.name).IsRequired().HasMaxLength(255);
            #endregion
        }
    }
}
