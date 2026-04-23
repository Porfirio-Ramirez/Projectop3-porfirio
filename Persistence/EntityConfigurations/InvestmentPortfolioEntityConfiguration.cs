using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;

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
