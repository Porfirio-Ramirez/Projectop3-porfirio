using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProyectoDeAprendizajeP3.Core.Domain.Entities;


namespace ProyectoDeAprendizajeP3.Infrastruture.Persistence.EntityConfigurations
{
    public class AssetHistoryEntityConfiguration : IEntityTypeConfiguration<AssetHistory>
    {
        public void Configure(EntityTypeBuilder<AssetHistory> builder)
        {
            #region Basic Configuration
            builder.HasKey(x => x.Id);
            builder.ToTable("AssetHistorical");
            #endregion

            #region Property Configuartion
            builder.Property(h => h.value).IsRequired().HasDefaultValue(0);
            #endregion


        }
    }
}
