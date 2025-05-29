using BookStore.Core.DataAccess.Concrete;
using BookStore.DataAccess.Abstract;
using BookStore.DataAccess.Context;
using BookStore.Dto.Concrete;
using BookStore.Entities.Concrete;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Concrete;

public class EfKitapIcerikRepo : EfGenericRepository<KitapIcerik> , IKitapIcerikDal
{
    private DataContext? context => _context as DataContext;
    public EfKitapIcerikRepo(DbContext context) : base(context)
    {
    }

    public async Task<List<KitapIcerikDto>> KitapIceriklerAsync()
    {
        var sartY=PredicateBuilder.New<Yazar>();
        var sartK = PredicateBuilder.New<Kitap>();
        var sartKI=PredicateBuilder.New<KitapIcerik>();

        sartY.And(x => x.AktifMi == 1 && x.SilindiMi == 0);
        sartK.And(x => x.AktifMi == 1 && x.SilindiMi == 0);
        sartKI.And(x => x.AktifMi == 1 && x.SilindiMi == 0);

        var query = from ki in context.KitapIcerikler.Where(sartKI)
                    join k in context.Kitaplar.Where(sartK)
                    on ki.KitapId equals k.Id
                    join y in context.Yazarlar.Where(sartY)
                    on ki.YazarId equals y.Id
                    select new KitapIcerikDto
                    {
                        Id = ki.Id,
                        YazarId = y.Id,
                        KitapId = k.Id,
                        KitapAdi=k.KitapAdi,
                        YazarAdi=y.YazarAdi,
                        Dosya=ki.Dosya,
                        AktifMi=ki.AktifMi,
                    };
        return await query.ToListAsync();
    }
}
