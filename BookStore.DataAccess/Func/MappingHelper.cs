using Microsoft.EntityFrameworkCore;
using BookStore.DataAccess.Mapping;

namespace BookStore.DataAccess.Func;

public static class MappingHelper
{
    public static void BookStoreMappingInfo(this ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("SCELIK");
        modelBuilder.OracleNameUpper();

        modelBuilder.ApplyConfiguration(new KitapIcerikMap());
        modelBuilder.ApplyConfiguration(new KitapMap());
        modelBuilder.ApplyConfiguration(new YazarMap());
    }


}

