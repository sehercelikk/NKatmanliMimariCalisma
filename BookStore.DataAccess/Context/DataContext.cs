using BookStore.DataAccess.Func;
using BookStore.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Context;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options){ }
    protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.BookStoreMappingInfo();

    public DbSet<Kitap> Kitaplar { get; set; }
    public DbSet<Yazar> Yazarlar { get; set; }
    public DbSet<KitapIcerik> KitapIcerikler { get; set; }

}
