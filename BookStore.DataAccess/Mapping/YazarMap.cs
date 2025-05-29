using BookStore.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.DataAccess.Mapping
{
    public class YazarMap : IEntityTypeConfiguration<Yazar>
    {
        public void Configure(EntityTypeBuilder<Yazar> builder)
        {
            builder.HasKey(x => x.Id).HasName("YAZAR_PK");

            builder.Property(x => x.Id).HasMaxLength(60);
            builder.Property(x => x.YazarAdi).HasMaxLength(150).IsRequired();
            builder.Property(x => x.SilmeZamani).IsRequired(false);
            builder.Property(x => x.PasifeAlmaZamani).IsRequired(false);

            builder.HasMany(a => a.Kitaplar)
                .WithOne(a => a.Yazar)
                .HasForeignKey(b => b.YazarId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(a => a.KitapIcerikler).WithOne(a => a.Yazar)
                .HasForeignKey(a => a.YazarId)
                .HasPrincipalKey(a => a.Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
