using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;

namespace Persistence.EntityConfigurations
{
    public class AssetTypeEntityConfiguration : IEntityTypeConfiguration<AssetType>
    {
        public void Configure(EntityTypeBuilder<AssetType> builder)
        {
            #region Basic Configuration
            builder.HasKey(x => x.Id);
            builder.ToTable("AssetTypes");
            #endregion

            #region Property Configuration
            builder.Property(t => t.name).IsRequired().HasMaxLength(255);
            #endregion

            #region Relationships
            builder.HasMany<Asset>(a => a.Assets)
                   .WithOne(t => t.AssetType)
                   .HasForeignKey(t => t.AssetTypeId)
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
