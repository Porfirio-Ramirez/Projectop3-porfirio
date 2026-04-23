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
    public class AssetEntityConfiguration : IEntityTypeConfiguration<Asset>
    {
        public void Configure(EntityTypeBuilder<Asset> builder)
        {
            // fluent api
            #region Basic Configuration
            builder.HasKey(a => a.Id);
            builder.ToTable("Assets");
            #endregion

            #region Property Configuration
            builder.Property(a => a.name).IsRequired().HasMaxLength(255);
            builder.Property(a => a.symbol).IsRequired().HasMaxLength(20);
            #endregion

            #region Relationships
            builder.HasMany<AssetHistory>(a => a.AssetHistories)
                   .WithOne(a => a.Assets)
                   .HasForeignKey(a => a.AssetId)
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }

       


    }
}
