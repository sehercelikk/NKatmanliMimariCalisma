using BookStore.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.DataAccess.Mapping;

public class KitapIcerikMap : IEntityTypeConfiguration<KitapIcerik>
{
    public void Configure(EntityTypeBuilder<KitapIcerik> builder)
    {
        builder.HasKey(x => x.Id).HasName("KTP_ICRK_PK");
        builder.Property(a => a.Id).HasMaxLength(60);

        builder.Property(a => a.YazarId).IsRequired();
        builder.Property(a=>a.KitapId).IsRequired();
        //builder.Property(a=>a.Baslik).IsRequired();

        builder.Property(x => x.SilmeZamani).IsRequired(false);
        builder.Property(x => x.PasifeAlmaZamani).IsRequired(false);

        builder.Property(e => e.Dosya).HasColumnType("CLOB").IsRequired(false);
    }
}
