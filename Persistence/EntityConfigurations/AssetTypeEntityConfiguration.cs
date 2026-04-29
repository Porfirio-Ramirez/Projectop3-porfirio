using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProyectoDeAprendizajeP3.Core.Domain.Entities;


namespace ProyectoDeAprendizajeP3.Infrastruture.Persistence.EntityConfigurations
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
