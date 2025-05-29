using BookStore.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Mapping
{
    internal class KitapMap : IEntityTypeConfiguration<Kitap>
    {
        public void Configure(EntityTypeBuilder<Kitap> builder)
        {
            builder.HasKey(x => x.Id).HasName("KITAP_PK");
            builder.Property(a => a.Id).HasMaxLength(60);

            builder.Property(x => x.KitapAdi).IsRequired();
            builder.Property(x => x.YazarId).IsRequired();
            builder.Property(x => x.SilmeZamani).IsRequired(false);
            builder.Property(x => x.PasifeAlmaZamani).IsRequired(false);

            builder.HasMany(a=>a.KitapIcerikler).WithOne(b=>b.Kitap)
                .HasForeignKey(a=>a.KitapId)
                .HasPrincipalKey(a=>a.Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
